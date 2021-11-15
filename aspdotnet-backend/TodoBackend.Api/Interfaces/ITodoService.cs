using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoViewModel>> GetTodos();
        Task<TodoViewModel> GetTodo(Guid guid);
        Task<TodoViewModel> CreateTodo(TodoViewModel todoView);
        Task<TodoViewModel> UpdateTodo(Guid guid, TodoViewModel todoView);
        bool DeleteTodo(Guid guid);
    }
}
