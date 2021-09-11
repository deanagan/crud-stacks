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
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var sql = @"
                    select u.Id,
                           u.UniqueId,
                           u.FirstName,
                           u.LastName,
                           u.Email,
                           u.Hash,
                           u.Created,
                           u.Updated,
                           r.Id,
                           r.UniqueId,
                           r.Kind,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id";

            using (var conn = new SqlConnection(_connectionString))
            {
                var users = await conn.QueryAsync<User, Role, User>(sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                });

                return users;
            }
        }

        public async Task<User> GetUserByGuid(Guid userGuid)
        {
            var sql = @"
                    select u.Id,
                           u.UniqueId,
                           u.FirstName,
                           u.LastName,
                           u.Email,
                           u.Hash,
                           u.Created,
                           u.Updated,
                           r.Id,
                           r.UniqueId,
                           r.Kind,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id
                    where u.UniqueId = @UserGuid";

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<User, Role, User>(sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                }, new { UserGuid = userGuid });

                return result.FirstOrDefault();
            }
        }

        public User AddUser(User user)
        {
            var sql = @"
                        declare @Outcome table (
                            Id int,
                            UniqueId uniqueidentifier,
                            Created datetime,
                            Updated datetime
                        );

                        select @RoleId = r.Id
                        from Roles as r with (nolock)
                        where r.UniqueId = @RoleUniqueId;

                        insert into dbo.Users (
                            [UniqueId],
                            [FirstName],
                            [LastName],
                            [Email],
                            [Hash],
                            [RoleId]
                        )
                        output inserted.Id, inserted.UniqueId, inserted.Created, inserted.Updated into @Outcome
                        values (
                            NEWID(),
                            @FirstName,
                            @LastName,
                            @Email,
                            @Hash,
                            @RoleId
                            );
                        select @Id = Id,
                               @UniqueId = UniqueId,
                               @UserCreated = Created,
                               @UserUpdated = Updated
                        from @Outcome;

                        select @Kind = r.Kind,
                               @Description = r.Description,
                               @RoleCreated = r.Created,
                               @RoleUpdated = r.Updated
                        from dbo.Roles as r
                        where r.Id = @RoleId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@FirstName", user.FirstName);
                parameter.Add("@LastName", user.LastName);
                parameter.Add("@Email", user.Email);
                parameter.Add("@Hash", user.Hash ?? "123456");
                parameter.Add("@RoleUniqueId", user.Role.UniqueId);

                parameter.Add("@Id", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@UniqueId", null, DbType.Guid, ParameterDirection.Output);
                parameter.Add("@Kind", null, DbType.String, ParameterDirection.Output, 150);
                parameter.Add("@Description", null, DbType.String, ParameterDirection.Output, -1);
                parameter.Add("@RoleId", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@RoleCreated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@RoleUpdated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@UserCreated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@UserUpdated", null, DbType.DateTime, ParameterDirection.Output);

                var result = conn.Execute(sql, parameter);

                var newUser = new User() {

                    Id = parameter.Get<int>("@Id"),
                    UniqueId = parameter.Get<Guid>("@UniqueId"),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Hash = user.Hash ?? "123456",
                    Created = parameter.Get<DateTime>("@UserCreated"),
                    Updated = parameter.Get<DateTime>("@UserUpdated"),
                    Role = new Role() {
                        Id = parameter.Get<int>("@RoleId"),
                        UniqueId = user.Role.UniqueId,
                        Kind = parameter.Get<string>("@Kind"),
                        Description = parameter.Get<string>("@Description"),
                        Created = parameter.Get<DateTime>("@RoleCreated"),
                        Updated = parameter.Get<DateTime>("@RoleUpdated")
                    }
                };

                return newUser;
            }
        }

        public User UpdateUser(Guid userGuid, User user)
        {
            var sql = @"
						if object_id('tempdb.#NewValues') is not null
						begin
						   drop table #NewValues
						end
						create table #NewValues
                        (
                            UniqueId uniqueidentifier,
                            FirstName nvarchar(100),
                            LastName nvarchar(100),
                            Email nvarchar(150),
                            [Hash] nvarchar(150),
                            Updated datetime,
                            RoleUniqueId uniqueidentifier
                        )

						insert into #NewValues(UniqueId, FirstName, LastName, Email, Hash, Updated, RoleUniqueId)
						Select @UniqueId, @FirstName, @LastName, @Email, @Hash, @UserUpdated, @RoleUniqueId

                        update u
                        set u.FirstName = @FirstName,
                            u.LastName = @LastName,
                            u.Email = @Email,
                            u.Hash = @Hash,
                            u.Updated = getutcdate(),
                            u.RoleId = r.Id
						from dbo.Users u
                            inner join dbo.Roles r on r.UniqueId = @RoleUniqueId
                        where u.UniqueId = @UniqueId
                        and exists
                            (
                            select u.FirstName,
                                   u.LastName,
                                   u.Email,
                                   u.Hash,
                                   u.Updated,
                                   r.UniqueId
                            except
                            select nv.FirstName,
                                   nv.LastName,
                                   nv.Email,
                                   nv.Hash,
                                   nv.Updated,
                                   nv.RoleUniqueId
                            from #NewValues nv
                            where nv.UniqueId = u.UniqueId
                            )

                        select @UserId = u.Id,
                               @UserCreated = u.Created,
                               @UserUpdated = u.Updated,
                               @RoleUniqueId = r.UniqueId,
                               @RoleId = r.Id,
                               @RoleKind = r.Kind,
                               @RoleDescription = r.Description,
                               @RoleCreated = r.Created,
                               @RoleUpdated = r.Updated
                        from dbo.Users as u with (nolock)
                            inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id
                        where u.UniqueId = @UniqueId;";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", userGuid);
                parameter.Add("@FirstName", user.FirstName);
                parameter.Add("@LastName", user.LastName);
                parameter.Add("@Email", user.Email);
                parameter.Add("@Hash", user.Hash ?? "123456");
                //parameter.Add("@RoleUniqueId", user.RoleUniqueId);

                parameter.Add("@UserId", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@UserCreated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@UserUpdated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@RoleId", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@RoleKind", null, DbType.String, ParameterDirection.Output, 150);
                parameter.Add("@RoleDescription", null, DbType.String, ParameterDirection.Output, -1);
                parameter.Add("@RoleCreated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@RoleUpdated", null, DbType.DateTime, ParameterDirection.Output);

                conn.Execute(sql, parameter);

                return new User
                {
                    Id = parameter.Get<int>("@UserId"),
                    UniqueId = userGuid,
                    // FirstName = User.FirstName,
                    // LastName = User.LastName,
                    // Email = User.Email,
                    // Hash = User.Hash ?? "123456",
                    Created = parameter.Get<DateTime>("@UserCreated"),
                    Updated = parameter.Get<DateTime>("@UserUpdated"),
                    // RoleId = parameter.Get<int>("@RoleId"),
                    // RoleUniqueId = parameter.Get<Guid>("@RoleUniqueId"),
                    // RoleKind = parameter.Get<string>("@RoleKind"),
                    // RoleDescription = parameter.Get<string>("@RoleDescription"),
                    // RoleCreated = parameter.Get<DateTime>("@RoleCreated"),
                    // RoleUpdated = parameter.Get<DateTime>("@RoleUpdated")
                };
            }
        }

        public bool DeleteUser(Guid userGuid)
        {
            var sql = @"
						delete u
                        from dbo.Users u
                        where u.UniqueId = @UniqueId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", userGuid);

                return conn.Execute(sql, parameter) != 0;
            }
        }
    }
}