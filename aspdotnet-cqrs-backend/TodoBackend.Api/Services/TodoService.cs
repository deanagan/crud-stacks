using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;

namespace TodoBackend.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepository, UserManager<User> userManager, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<TodoViewModel> CreateTodo(TodoViewModel todoView)
        {
            var todo = _mapper.Map<Todo>(todoView);
            var newTodo = _todoRepository.AddTodo(todo);
            var assignedUser = await GetAssigneeView(todoView.Assignee?.UniqueId ?? Guid.Empty);

            return new TodoViewModel()
            {
                UniqueId = newTodo.UniqueId,
                Summary = newTodo.Summary,
                Detail = newTodo.Detail,
                IsDone = newTodo.IsDone,
                Created = newTodo.Created,
                Updated = newTodo.Updated,
                Assignee = assignedUser
            };
        }

        public async Task<TodoViewModel> GetTodo(Guid guid)
        {
            var todo = await _todoRepository.GetTodoByGuid(guid);
            var user = await GetAssigneeView(todo.AssigneeGuid);

            return new TodoViewModel()
            {
                UniqueId = todo.UniqueId,
                Summary = todo.Summary,
                Detail = todo.Detail,
                IsDone = todo.IsDone,
                Created = todo.Created,
                Updated = todo.Updated,
                Assignee = user
            };
        }

        public async Task<IEnumerable<TodoViewModel>> GetTodos()
        {
            try
            {
                var todos = await _todoRepository.GetAllTodos();
                var usersLookup = _userManager.Users.ToDictionary(key => key.UniqueId, value => value);

                var todoViews = todos.Select(todo => new TodoViewModel()
                {
                    UniqueId = todo.UniqueId,
                    Summary = todo.Summary,
                    Detail = todo.Detail,
                    IsDone = todo.IsDone,
                    Created = todo.Created,
                    Updated = todo.Updated,
                    Assignee = todo.AssigneeGuid != Guid.Empty ? new AssigneeViewModel()
                    {
                        UniqueId = usersLookup[todo.AssigneeGuid].UniqueId,
                        FirstName = usersLookup[todo.AssigneeGuid].FirstName,
                        LastName = usersLookup[todo.AssigneeGuid].LastName
                    } : null
                });

                return todoViews;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to get todos");
                return null;
            }
        }

        private async Task<AssigneeViewModel> GetAssigneeView(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user == null)
            {
                return null;
            }

            return new AssigneeViewModel()
            {
                UniqueId = user.UniqueId,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<TodoViewModel> UpdateTodo(Guid guid, TodoViewModel todoView)
        {
            var todoForUpdate = await _todoRepository.GetTodoByGuid(guid);

            todoForUpdate.Summary = todoView.Summary ?? todoForUpdate.Summary;
            todoForUpdate.Detail = todoView.Detail ?? todoForUpdate.Detail;
            todoForUpdate.IsDone = todoView.IsDone.HasValue ? todoView.IsDone.Value : todoForUpdate.IsDone;
            todoForUpdate.AssigneeGuid = todoView.Assignee?.UniqueId ?? todoForUpdate.AssigneeGuid;

            var updatedTodo = _todoRepository.UpdateTodo(guid, todoForUpdate);
            var assigneeGuid = updatedTodo.AssigneeGuid;
            var user = await GetAssigneeView(assigneeGuid);

            return new TodoViewModel()
            {
                UniqueId = updatedTodo.UniqueId,
                Summary = updatedTodo.Summary,
                Detail = updatedTodo.Detail,
                IsDone = updatedTodo.IsDone,
                Created = updatedTodo.Created,
                Updated = updatedTodo.Updated,
                Assignee = user
            };
        }

        public bool DeleteTodo(Guid guid)
        {
            return _todoRepository.DeleteTodo(guid);
        }
    }
}
