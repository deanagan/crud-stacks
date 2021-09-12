using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllTodos();
        Task<Todo> GetTodoByGuid(Guid guid);
        Todo AddTodo(Todo todo);
        Todo UpdateTodo(Guid guid, Todo todo);
        bool DeleteTodo(Guid guid);
    }
}
