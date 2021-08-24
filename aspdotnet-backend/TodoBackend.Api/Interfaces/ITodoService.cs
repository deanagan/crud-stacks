using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetTodosAsync();
        IEnumerable<Todo> GetTodos();
        Task<Todo> GetTodo(int id);
        void CreateTodo(Todo Todo);
        bool UpdateTodo(Todo Todo);
        bool DeleteTodo(int id);
    }

}
