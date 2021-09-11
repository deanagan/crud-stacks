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
                           t.AssigneeId
                    from dbo.Todo as t with (nolock)
                        left join dbo.Users as u with (nolock) on t.AssigneeId = u.Id";

            using (var conn = new SqlConnection(_connectionString))
            {
                var users = await conn.QueryAsync<Todo>(sql);
                return users;
            }
        }

        Todo ITodoRepository.AddUser(Todo todo)
        {
            throw new NotImplementedException();
        }

        bool ITodoRepository.DeleteUser(Guid userGuid)
        {
            throw new NotImplementedException();
        }

        Task<Todo> ITodoRepository.GetTodoByGuid(Guid userGuid)
        {
            throw new NotImplementedException();
        }

        Todo ITodoRepository.UpdateUser(Guid userGuid, Todo todo)
        {
            throw new NotImplementedException();
        }
    }
}