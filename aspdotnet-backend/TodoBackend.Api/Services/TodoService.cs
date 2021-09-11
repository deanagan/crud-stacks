using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;


namespace TodoBackend.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public TodoService(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        TodoView ITodoService.CreateTodo(TodoView todoView)
        {
            throw new NotImplementedException();
        }

        bool ITodoService.DeleteTodo(Guid guid)
        {
            throw new NotImplementedException();
        }

        Task<TodoView> ITodoService.GetTodo(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TodoView>> GetTodos()
        {
            var todos = await _todoRepository.GetAllTodos();
            var users = await _userRepository.GetAllUsers();

            var userIdLookup = users.ToDictionary(key => key.Id, value => value);

            var todoViews = todos.Select(todo => new TodoView() {
                UniqueId = todo.UniqueId,
                Summary = todo.Summary,
                Detail = todo.Detail,
                IsDone = todo.IsDone,
                Created = todo.Created,
                Updated = todo.Updated,
                Assignee = todo.AssigneeId != null ? new AssigneeView() {
                    UniqueId = userIdLookup[todo.AssigneeId.Value].UniqueId,
                    FirstName = userIdLookup[todo.AssigneeId.Value].FirstName,
                    LastName = userIdLookup[todo.AssigneeId.Value].LastName
                } : null
            });

            return todoViews;
        }

        TodoView ITodoService.UpdateTodo(Guid guid, TodoView todoView)
        {
            throw new NotImplementedException();
        }
    }
}
