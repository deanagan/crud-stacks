using System.Collections.Generic;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Dtos;

using System.Threading.Tasks;
using AutoMapper;
using System;

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

        Todo ITodoService.CreateTodo(Todo Todo)
        {
            throw new NotImplementedException();
        }

        bool ITodoService.DeleteTodo(Guid guid)
        {
            throw new NotImplementedException();
        }

        Task<Todo> ITodoService.GetTodo(Guid guid)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Todo>> ITodoService.GetTodos()
        {
            throw new NotImplementedException();
        }

        Todo ITodoService.UpdateTodo(Guid gud, Todo Todo)
        {
            throw new NotImplementedException();
        }
    }
}
