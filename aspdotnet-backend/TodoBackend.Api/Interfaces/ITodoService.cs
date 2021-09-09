using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> GetTodo(Guid guid);
        Todo CreateTodo(Todo Todo);
        Todo UpdateTodo(Guid guid, Todo Todo);
        bool DeleteTodo(Guid guid);
    }
}
