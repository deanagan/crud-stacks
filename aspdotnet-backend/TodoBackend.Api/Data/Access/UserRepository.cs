using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using Dapper;

using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Dtos;
using System;

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

        Task<UserDto> IUserRepository.GetUserByGuid(Guid user)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.AddUser(UserDto parameter)
        {
            throw new NotImplementedException();
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