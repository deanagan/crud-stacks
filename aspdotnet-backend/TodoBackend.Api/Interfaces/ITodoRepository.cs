using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Dtos;

namespace TodoBackend.Api.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoDto>> GetAllTodos();
        Task<TodoDto> GetTodoByGuid(Guid userGuid);
        TodoDto AddUser(TodoDto parameter);
        TodoDto UpdateUser(Guid userGuid, TodoDto userDto);
        bool DeleteUser(Guid userGuid);
    }
}
