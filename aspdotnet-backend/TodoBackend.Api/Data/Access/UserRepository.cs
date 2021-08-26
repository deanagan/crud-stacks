using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

using Dapper;

using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Dtos;

namespace TodoBackend.Api.Data.Access
{
    public class UserRepository : IDataRepository<UserDto>
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        private string GetQueryString()
        {
            return @"
                    select u.Id,
                           u.Name,
                           u.Email,
                           u.Hash,
                           r.Id as RoleId,
                           r.Name as RoleName,
                           r.Description
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id";
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<UserDto>(GetQueryString());
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<UserDto>(GetQueryString());
            }
        }

        public void Add(UserDto parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(UserDto parameter)
        {
            throw new System.NotImplementedException();
        }

        public void AddRange(IEnumerable<UserDto> parameters)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(UserDto parameter)
        {
            throw new System.NotImplementedException();
        }

        public UserDto Get(params object[] parameters)
        {
            throw new System.NotImplementedException();
        }


        public Task<UserDto> GetAsync(params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public void Update(UserDto parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}