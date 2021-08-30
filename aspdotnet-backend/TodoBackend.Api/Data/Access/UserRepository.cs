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
                           r.Id as RoleId,
                           r.UniqueId as RoleUniqueId,
                           r.Kind as RoleKind,
                           r.Description as RoleDescription,
                           r.Created as RoleCreated
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
                           r.Id as RoleId,
                           r.UniqueId as RoleUniqueId,
                           r.Kind as RoleKind,
                           r.Description as RoleDescription,
                           r.Created as RoleCreated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id
                    where u.UniqueId = @UserGuid";

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<UserDto>(sql, new { UserGuid = userGuid });
                return result.FirstOrDefault();
            }
        }

        public void AddUser(UserDto userDto)
        {
            var sql = @"
                        declare @Outcome table (
                            UniqueId uniqueidentifier,
                            Created datetime
                        );
                        insert into dbo.Users (
                            [UniqueId],
                            [FirstName],
                            [LastName],
                            [Email],
                            [Hash],
                            [RoleId]
                        )
                        output inserted.UniqueId, inserted.Created into @Outcome
                        values (
                            NEWID(),
                            @FirstName,
                            @LastName,
                            @Email,
                            @Hash,
                            @RoleId
                            );
                        select @UniqueId = UniqueId, @UserCreated = Created
                        from @Outcome;

                        select @RoleUniqueId = r.UniqueId,
                               @Kind = r.Kind,
                               @Description = r.Description,
                               @RoleCreated = r.Created
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
                parameter.Add("@UserCreated", null, DbType.DateTime, ParameterDirection.Output);
                conn.Execute(sql, parameter);

                userDto.UniqueId = parameter.Get<Guid>("@UniqueId");
                userDto.RoleUniqueId = parameter.Get<Guid>("@RoleUniqueId");
                userDto.RoleKind = parameter.Get<string>("@Kind");
                userDto.RoleDescription = parameter.Get<string>("@Description");
                userDto.RoleCreated = parameter.Get<DateTime>("@RoleCreated");
                userDto.Created = parameter.Get<DateTime>("@UserCreated");
            }
        }

        void IUserRepository.AddUsers(IEnumerable<UserDto> userGuids)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.UpdateUser(Guid userGuid)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.DeleteUser(Guid userGuid)
        {
            throw new NotImplementedException();
        }
    }
}