using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllTodos();
        Task<Todo> GetTodoByGuid(Guid userGuid);
        Todo AddUser(Todo todo);
        Todo UpdateUser(Guid userGuid, Todo todo);
        bool DeleteUser(Guid userGuid);
    }
}
