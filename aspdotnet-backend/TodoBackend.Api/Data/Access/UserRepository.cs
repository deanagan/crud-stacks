using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.Extensions.Configuration;

using Dapper;

using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Dtos;


namespace TodoBackend.Api.Data.Access
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
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
                           r.Id as RoleId,
                           r.UniqueId as RoleUniqueId,
                           r.Kind as RoleKind,
                           r.Description as RoleDescription,
                           r.Created as RoleCreated,
                           r.Updated as RoleUpdated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<UserDto>(sql);
            }
        }

        public async Task<UserDto> GetUserByGuid(Guid userGuid)
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
                           r.Id as RoleId,
                           r.UniqueId as RoleUniqueId,
                           r.Kind as RoleKind,
                           r.Description as RoleDescription,
                           r.Created as RoleCreated,
                           r.Updated as RoleUpdated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id
                    where u.UniqueId = @UserGuid";

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<UserDto>(sql, new { UserGuid = userGuid });
                return result.FirstOrDefault();
            }
        }

        public UserDto AddUser(UserDto userDto)
        {
            var sql = @"
                        declare @Outcome table (
                            UniqueId uniqueidentifier,
                            Created datetime,
                            Updated datetime
                        );
                        insert into dbo.Users (
                            [UniqueId],
                            [FirstName],
                            [LastName],
                            [Email],
                            [Hash],
                            [RoleId]
                        )
                        output inserted.UniqueId, inserted.Created, inserted.Updated into @Outcome
                        values (
                            NEWID(),
                            @FirstName,
                            @LastName,
                            @Email,
                            @Hash,
                            @RoleId
                            );
                        select @UniqueId = UniqueId,
                               @UserCreated = Created,
                               @UserUpdated = Updated
                        from @Outcome;

                        select @RoleUniqueId = r.UniqueId,
                               @Kind = r.Kind,
                               @Description = r.Description,
                               @RoleCreated = r.Created,
                               @RoleUpdated = r.Updated
                        from dbo.Roles as r
                        where r.Id = @RoleId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", null, DbType.Guid, ParameterDirection.Output);
                parameter.Add("@FirstName", userDto.FirstName);
                parameter.Add("@LastName", userDto.LastName);
                parameter.Add("@Email", userDto.Email);
                parameter.Add("@Hash", userDto.Hash ?? "123456");
                parameter.Add("@RoleId", userDto.RoleId);
                parameter.Add("@RoleUniqueId", null, DbType.Guid, ParameterDirection.Output);
                parameter.Add("@Kind", null, DbType.String, ParameterDirection.Output, 150);
                parameter.Add("@Description", null, DbType.String, ParameterDirection.Output, -1);
                parameter.Add("@RoleCreated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@RoleUpdated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@UserCreated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@UserUpdated", null, DbType.DateTime, ParameterDirection.Output);
                conn.Execute(sql, parameter);

                var newUserDto = userDto.Clone();
                newUserDto.UniqueId = parameter.Get<Guid>("@UniqueId");
                newUserDto.RoleUniqueId = parameter.Get<Guid>("@RoleUniqueId");
                newUserDto.RoleKind = parameter.Get<string>("@Kind");
                newUserDto.RoleDescription = parameter.Get<string>("@Description");
                newUserDto.RoleCreated = parameter.Get<DateTime>("@RoleCreated");
                newUserDto.RoleUpdated = parameter.Get<DateTime>("@RoleUpdated");
                newUserDto.Created = parameter.Get<DateTime>("@UserCreated");
                newUserDto.Updated = parameter.Get<DateTime>("@UserUpdated");

                return newUserDto;
            }
        }

        void IUserRepository.AddUsers(IEnumerable<UserDto> userGuids)
        {
            throw new NotImplementedException();
        }

        public UserDto UpdateUser(Guid userGuid, UserDto userDto)
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
                            RoleId int
                        )

                        insert into #NewValues
                            (UniqueId, FirstName, LastName, Email, Hash, Updated, RoleId)
                        Select @UniqueId, @FirstName, @LastName, @Email, @Hash, @Updated, @RoleId

                        if object_id('tempdb.#Outcome') is not null
                        begin
                            drop table #Outcome
                        end

                        create table #Outcome
                        (
                            UserId int,
                            UserCreated datetime,
                            UserUpdated datetime,
                            RoleUniqueId uniqueidentifier,
                            RoleKind nvarchar(150),
                            RoleDescription nvarchar(max),
                            RoleCreated datetime,
                            RoleUpdated datetime
                        );

                        update u
                            set u.FirstName = @FirstName,
                                u.LastName = @LastName,
                                u.Email = @Email,
                                u.Hash = @Hash,
                                u.Updated = getutcdate(),
                                u.RoleId = @RoleId
                            from dbo.Users u
                            where u.UniqueId = @UniqueId
                            and exists
                        (
                                select u.FirstName,
                                    u.LastName,
                                    u.Email,
                                    u.Hash,
                                    u.Updated,
                                    u.RoleId
                                except
                                select nv.FirstName,
                                    nv.LastName,
                                    nv.Email,
                                    nv.Hash,
                                    nv.Updated,
                                    nv.RoleId
                                from #NewValues nv
                                where nv.UniqueId = u.UniqueId
                        )

                        insert into #Outcome(UserId, UserCreated, UserUpdated, RoleUniqueId, RoleKind, RoleDescription, RoleCreated, RoleUpdated)
                        select u.Id,
                            u.Created,
                            u.Updated,
                            r.UniqueId,
                            r.Kind,
                            r.Description,
                            r.Created as RoleCreated,
                            r.Updated as RoleUpdated
                        from dbo.Users as u with (nolock)
                            inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id
                        where u.UniqueId = @UniqueId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", userGuid);
                parameter.Add("@FirstName", userDto.FirstName);
                parameter.Add("@LastName", userDto.LastName);
                parameter.Add("@Email", userDto.Email);
                parameter.Add("@Hash", userDto.Hash);
                parameter.Add("@RoleId", userDto.RoleId);

                parameter.Add("@UserId", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@UserCreated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@UserUpdated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@RoleUniqueId", null, DbType.Guid, ParameterDirection.Output);
                parameter.Add("@RoleKind", null, DbType.String, ParameterDirection.Output, 150);
                parameter.Add("@RoleDescription", null, DbType.String, ParameterDirection.Output, -1);
                parameter.Add("@RoleCreated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@RoleUpdated", null, DbType.DateTime, ParameterDirection.Output);

                conn.Execute(sql, parameter);

                return new UserDto {
                    Id = userDto.Id,
                    UniqueId = userGuid,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    Hash = userDto.Hash,
                    Created = userDto.Created,
                    Updated = userDto.Updated,
                    RoleId = userDto.RoleId,
                    RoleUniqueId = parameter.Get<Guid>("RoleUniqueId"),
                    RoleKind = parameter.Get<string>("@RoleKind"),
                    RoleDescription = parameter.Get<string>("@RoleDescription"),
                    RoleCreated = parameter.Get<DateTime>("@RoleCreated"),
                    RoleUpdated = parameter.Get<DateTime>("@RoleUpdate")
                };
            }
        }

        void IUserRepository.DeleteUser(Guid userGuid)
        {
            throw new NotImplementedException();
        }
    }
}