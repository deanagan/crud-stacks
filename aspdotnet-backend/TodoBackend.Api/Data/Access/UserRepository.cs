using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

using Dapper;

using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Models;



namespace TodoBackend.Api.Data.Access
{
    public class UserRepository : IDataRepository<User>
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public IEnumerable<User> GetAll()
        {
            const string sql = @"
                    select u.Name,
                           u.Email,
                           r.Name as Role,
                           r.Description
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id";

            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<User>(sql);
            }
        }

        public void Add(User parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(User parameter)
        {
            throw new System.NotImplementedException();
        }

        public void AddRange(IEnumerable<User> parameters)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(User parameter)
        {
            throw new System.NotImplementedException();
        }

        public User Get(params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetAsync(params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}