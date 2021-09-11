using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoView>> GetTodos();
        Task<TodoView> GetTodo(Guid guid);
        TodoView CreateTodo(TodoView todoView);
        TodoView UpdateTodo(Guid guid, TodoView todoView);
        bool DeleteTodo(Guid guid);
    }
}
