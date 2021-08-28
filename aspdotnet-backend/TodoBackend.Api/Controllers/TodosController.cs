using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Models;


namespace TodoBackend.Api.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class TodosController : ControllerBase
    {
        private ITodoService _todoService;
        private readonly ILogger<TodosController> _logger;

        public TodosController(ILogger<TodosController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            try
            {
                var result = await _todoService.GetTodosAsync();
                if (result == null)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            try
            {
                var result = await _todoService.GetTodo(id);
                if (result == null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateTodo(Todo todo)
        {
            if (todo != null)
            {
                try
                {
                    _todoService.CreateTodo(todo);
                    return CreatedAtAction(nameof(GetTodos), new { Id = todo.TodoId }, todo);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("todo creation failed.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, Todo todo)
        {
            if (todo != null)
            {
                try
                {
                    todo.TodoId = id;
                    if (_todoService.UpdateTodo(todo))
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest($"Todo with {id} not found.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("Todo is null");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            try
            {
                if (_todoService.DeleteTodo(id))
                {
                    return NoContent();
                }
                else
                {
                    // 401 if not authorised, else 404. 404 for now knowing we will
                    // have authentication eventually.
                    return NotFound("id does not exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
