using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;

namespace TodoBackend.Api.Features.Todos
{
  public class GetTodos
  {
    public class Query : IRequest<IEnumerable<TodoViewModel>> { }
    public class QueryHandler: IRequestHandler<Query, IEnumerable<TodoViewModel>>
    {
      private readonly ITodoService _todoService;
      public QueryHandler(ITodoService todoService)
      {
        _todoService = todoService;
      }

      public async Task<IEnumerable<TodoViewModel>> Handle(Query request, CancellationToken cancellationToken)
      {
        var result = await _todoService.GetTodos();

        return result;
      }
    }
  }
}
