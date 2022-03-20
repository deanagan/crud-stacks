using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Features.Todos
{
  [ApiController]
  [Route("api/[controller]")]
  public class TodosController : ControllerBase
  {
    private readonly IMediator _mediator;
    private readonly ILogger<TodosController> _logger;

    public TodosController(IMediator mediator, ILogger<TodosController> logger)
    {
      _mediator = mediator;
      _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        try
        {
            var response = await _mediator.Send(new GetTodos.Query());

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
  }
}
