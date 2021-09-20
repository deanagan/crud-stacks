using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Data.Access
{
    public class TodoRepository : ITodoRepository
    {
        private readonly string _connectionString;
        public TodoRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            var sql = @"
                    select t.Id,
                           t.UniqueId,
                           t.Summary,
                           t.Detail,
                           t.IsDone,
                           t.Created,
                           t.Updated,
                           t.AssigneeGuid
                    from dbo.Todo as t with (nolock)
                        left join dbo.Users as u with (nolock) on t.AssigneeGuid = u.UniqueId";

            using (var conn = new SqlConnection(_connectionString))
            {
                var users = await conn.QueryAsync<Todo>(sql);
                return users;
            }
        }

        public async Task<Todo> GetTodoByGuid(Guid guid)
        {
            var sql = @"
                    select t.Id,
                           t.UniqueId,
                           t.Summary,
                           t.Detail,
                           t.IsDone,
                           t.Created,
                           t.Updated,
                           t.AssigneeGuid
                    from dbo.Todo as t with (nolock)
                        left join dbo.Users as u with (nolock) on t.AssigneeGuid = u.UniqueId
                    where t.UniqueId = @Guid";

            using (var conn = new SqlConnection(_connectionString))
            {
                var users = await conn.QueryAsync<Todo>(sql, new {Guid = guid});
                return users.FirstOrDefault();
            }
        }

        public Todo AddTodo(Todo todo)
        {
            var sql = @"
                        declare @Outcome table (
                            Id int,
                            UniqueId uniqueidentifier,
                            Created datetime,
                            Updated datetime
                        );

                        insert into dbo.Todo (
                            [UniqueId],
                            [Summary],
                            [Detail],
                            [IsDone],
                            [AssigneeGuid]
                        )
                        output inserted.Id, inserted.UniqueId, inserted.Created, inserted.Updated into @Outcome
                        values (
                            NEWID(),
                            @Summary,
                            @Detail,
                            @IsDone,
                            @AssigneeGuid
                            );

                        select @Id = Id,
                               @UniqueId = UniqueId,
                               @Created = Created,
                               @Updated = Updated
                        from @Outcome;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();

                parameter.Add("@Summary", todo.Summary);
                parameter.Add("@Detail", todo.Detail);
                parameter.Add("@IsDone", todo.IsDone);
                parameter.Add("@AssigneeGuid", todo.AssigneeGuid == Guid.Empty ? null : todo.AssigneeGuid);

                parameter.Add("@Id", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@UniqueId", null, DbType.Guid, ParameterDirection.Output);
                parameter.Add("@Created", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@Updated", null, DbType.DateTime, ParameterDirection.Output);

                var result = conn.Execute(sql, parameter);

                return new Todo()
                {
                    Id = parameter.Get<int>("@Id"),
                    UniqueId = parameter.Get<Guid>("@UniqueId"),
                    Summary = todo.Summary,
                    Detail = todo.Detail,
                    IsDone = todo.IsDone,
                    Created = parameter.Get<DateTime>("@Created"),
                    Updated = parameter.Get<DateTime>("@Updated"),
                    AssigneeGuid = todo.AssigneeGuid
                };
            }
        }

        bool ITodoRepository.DeleteTodo(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Todo UpdateTodo(Guid guid, Todo todo)
        {
             var sql = @"
                        if object_id('tempdb.#NewValues') is not null
                        begin
                           drop table #NewValues
                        end

                        create table #NewValues
                        (
                            UniqueId uniqueidentifier,
                            Summary nvarchar(100),
                            Detail nvarchar(max),
                            IsDone bit,
                            Updated datetime,
                            AssigneeGuid uniqueidentifier
                        )

                        declare @Outcome table (
                            Id int,
                            Updated datetime,
                            Created datetime
                        )

                        insert into #NewValues(UniqueId, Summary, Detail, IsDone, AssigneeGuid)
                        Select @UniqueId, @Summary, @Detail, @IsDone, @AssigneeGuid

                        update t
                        set t.Summary = coalesce(nv.Summary, t.Summary),
                            t.Detail = coalesce(nv.Detail, t.Detail),
                            t.IsDone = coalesce(nv.IsDone, t.IsDone),
                            t.Updated = getutcdate(),
                            t.AssigneeGuid = nv.AssigneeGuid
                        output inserted.Id, inserted.Created, inserted.Updated into @Outcome
                        from dbo.Todo t
                            left join #NewValues nv
                            on nv.UniqueId = t.UniqueId
                            where t.UniqueId = @UniqueId

                        select @Id = Id,
                               @Created = Created,
                               @Updated = Updated
                        from @Outcome;
                            ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", guid);
                parameter.Add("@Summary", todo.Summary);
                parameter.Add("@Detail", todo.Detail);
                parameter.Add("@IsDone", todo.IsDone);
                parameter.Add("@AssigneeGuid", todo.AssigneeGuid == Guid.Empty ? null : todo.AssigneeGuid);

                parameter.Add("@Id", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@Created", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@Updated", null, DbType.DateTime, ParameterDirection.Output);

                conn.Execute(sql, parameter);

                return new Todo()
                {
                    Id = parameter.Get<int>("@Id"),
                    UniqueId = guid,
                    Summary = todo.Summary,
                    Detail = todo.Detail,
                    IsDone = todo.IsDone,
                    Created = parameter.Get<DateTime>("@Created"),
                    Updated = parameter.Get<DateTime>("@Updated"),
                    AssigneeGuid = todo.AssigneeGuid
                };
            }
        }

        public bool DeleteTodo(Guid guid)
        {
            var sql = @"
                        delete t
                        from dbo.Todo t
                        where t.UniqueId = @UniqueId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", guid);

                return conn.Execute(sql, parameter) != 0;
            }
        }
    }
}