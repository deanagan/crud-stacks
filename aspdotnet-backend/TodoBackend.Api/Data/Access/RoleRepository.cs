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


namespace TodoBackend.Api.Data.Access
{
    public class RolesRepository : IRolesRepository
    {
        private readonly string _connectionString;
        public RolesRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
             var sql = @"
                    select r.Id,
                           r.UniqueId,
                           r.Kind,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Roles as r with (nolock)";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<Role>(sql);
            }
        }

        public async Task<Role> GetRoleByGuid(Guid guid)
        {
            var sql = @"
                    select r.Id,
                           r.UniqueId,
                           r.Kind,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Roles as r with (nolock)
                        where r.UniqueId = @UniqueId";

            using (var conn = new SqlConnection(_connectionString))
            {
                var Role = await conn.QueryAsync<Role>(sql, new { UniqueId = guid });
                return Role.FirstOrDefault();
            }
        }

        public Role AddRole(Role role)
        {
            var sql = @"
                        declare @Outcome table (
                            Id int,
                            UniqueId uniqueidentifier,
                            Created datetime,
                            Updated datetime
                        );
                        insert into dbo.Roles (UniqueId, Kind, Description)
                        output inserted.Id, inserted.UniqueId, inserted.Created, inserted.Updated into @Outcome
                        values (NEWID(), @Kind, @Description);

                        select @Id = Id,
                               @UniqueId = UniqueId,
                               @Created = Created,
                               @Updated = Updated
                        from @Outcome;
                        ";


            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@UniqueId", null, DbType.Guid, ParameterDirection.Output);
                parameter.Add("@Created", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@Updated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@Kind", role.Kind);
                parameter.Add("@Description", role.Description);

                conn.Execute(sql, parameter);

                var newRole = new Role()
                {
                    Id = parameter.Get<int>("@Id"),
                    UniqueId = parameter.Get<Guid>("@UniqueId"),
                    Kind = role.Kind,
                    Description = role.Description,
                    Created = parameter.Get<DateTime>("@Created"),
                    Updated = parameter.Get<DateTime>("@Updated")
                };

                return newRole;
            }
        }

        Role IRolesRepository.UpdateRole(Guid guid, Role role)
        {
            var sql = @"
                        declare @Outcome table (
                            Id int,
                            Created datetime,
                            Updated datetime
                        );

                        update r
                        set r.Kind = @Kind,
                            r.Description = @Description,
                            r.Updated = getutcdate()
                        output inserted.Id, inserted.Created, inserted.Updated into @Outcome
                        from dbo.Roles r
                            where r.UniqueId = @UniqueId

                        select @Id = Id,
                               @Created = Created,
                               @Updated = Updated
                        from @Outcome;
                        ";


            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@Created", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@Updated", null, DbType.DateTime, ParameterDirection.Output);

                parameter.Add("@UniqueId", guid);
                parameter.Add("@Kind", role.Kind);
                parameter.Add("@Description", role.Description);

                conn.Execute(sql, parameter);

                var updatedRole = new Role()
                {
                    Id = parameter.Get<int>("@Id"),
                    UniqueId = guid,
                    Kind = role.Kind,
                    Description = role.Description,
                    Created = parameter.Get<DateTime>("@Created"),
                    Updated = parameter.Get<DateTime>("@Updated")
                };

                return updatedRole;
            }
        }

        public bool DeleteRole(Guid guid)
        {
            var sql = @"
                        delete r
                        from dbo.Roles r
                        where r.UniqueId = @UniqueId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", guid);

                var s = conn.Execute(sql, parameter) != 0;
                return s;
            }
        }
    }
}