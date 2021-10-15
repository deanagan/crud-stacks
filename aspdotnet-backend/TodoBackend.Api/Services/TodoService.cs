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
        private readonly IMapper _mapper;
        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<TodoViewModel> CreateTodo(TodoViewModel todoView)
        {
            var todo = _mapper.Map<Todo>(todoView);
            var newTodo = _todoRepository.AddTodo(todo);
            //var assignedUser = newTodo.AssigneeGuid != Guid.Empty ? await _userRepository.GetUserByGuid(newTodo.AssigneeGuid) : null;

            return new TodoViewModel()
            {
                UniqueId = newTodo.UniqueId,
                Summary = newTodo.Summary,
                Detail = newTodo.Detail,
                IsDone = newTodo.IsDone,
                Created = newTodo.Created,
                Updated = newTodo.Updated,
                Assignee = null
                // Assignee = assignedUser != null ? new AssigneeViewModel()
                // {
                //     UniqueId = assignedUser.UniqueId,
                //     FirstName = assignedUser.FirstName,
                //     LastName = assignedUser.LastName
                // } : null
            };
        }

        public async Task<TodoViewModel> GetTodo(Guid guid)
        {
            var todo = await _todoRepository.GetTodoByGuid(guid);
            // var user = await _userRepository.GetUserByGuid(todo.AssigneeGuid);

            return new TodoViewModel() {
                UniqueId = todo.UniqueId,
                Summary = todo.Summary,
                Detail = todo.Detail,
                IsDone = todo.IsDone,
                Created = todo.Created,
                Updated = todo.Updated,
                Assignee = null
                // user != null ? new AssigneeViewModel() {
                //     UniqueId = user.UniqueId,
                //     FirstName = user.FirstName,
                //     LastName = user.LastName
                // } : null
            };
        }

        public async Task<IEnumerable<TodoViewModel>> GetTodos()
        {
            var todos = await _todoRepository.GetAllTodos();
            //var users = await _userRepository.GetUsersByGuids(todos.Select(todo => todo.AssigneeGuid));
            //var userIdLookup = users.ToDictionary(key => key.UniqueId, value => value);

            var todoViews = todos.Select(todo => new TodoViewModel() {
                UniqueId = todo.UniqueId,
                Summary = todo.Summary,
                Detail = todo.Detail,
                IsDone = todo.IsDone,
                Created = todo.Created,
                Updated = todo.Updated,
                Assignee = null//todo.AssigneeGuid != Guid.Empty ? new AssigneeViewModel() {
                //     UniqueId = userIdLookup[todo.AssigneeGuid].UniqueId,
                //     FirstName = userIdLookup[todo.AssigneeGuid].FirstName,
                //     LastName = userIdLookup[todo.AssigneeGuid].LastName
                // } : null
            });

            return todoViews;
        }

        public async Task<TodoViewModel> UpdateTodo(Guid guid, TodoViewModel todoView)
        {
            var todo = _mapper.Map<Todo>(todoView);
            var updatedTodo = _todoRepository.UpdateTodo(guid, todo);
            //var user = (updatedTodo.AssigneeGuid != Guid.Empty) ?
                //await _userRepository.GetUserByGuid(updatedTodo.AssigneeGuid) : null;

            return new TodoViewModel()
            {
                UniqueId = updatedTodo.UniqueId,
                Summary = updatedTodo.Summary,
                Detail = updatedTodo.Detail,
                IsDone = updatedTodo.IsDone,
                Created = updatedTodo.Created,
                Updated = updatedTodo.Updated,
                Assignee = null //user != null ? new AssigneeViewModel()
                // {
                //     UniqueId = user.UniqueId,
                //     FirstName = user.FirstName,
                //     LastName = user.LastName
                // } : null
            };
        }

        public bool DeleteTodo(Guid guid)
        {
            return _todoRepository.DeleteTodo(guid);
        }
    }
}
