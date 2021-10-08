using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.ViewModels;


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
                var result = await _todoService.GetTodos();
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

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetTodo(Guid guid)
        {
            try
            {
                var result = await _todoService.GetTodo(guid);
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
        public async Task<IActionResult> CreateTodo(TodoViewModel todoView)
        {
            if (todoView != null)
            {
                try
                {
                    var newTodoView = await _todoService.CreateTodo(todoView);
                    return CreatedAtAction(nameof(GetTodos), new { Id = newTodoView.UniqueId }, newTodoView);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("todo creation failed.");
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateTodo(Guid guid, TodoViewModel todoView)
        {
            if (todoView != null)
            {
                try
                {
                    var updatedTodo = await _todoService.UpdateTodo(guid, todoView);
                    if (updatedTodo != null)
                    {
                        return Ok(updatedTodo);
                    }
                    else
                    {
                        return BadRequest($"Todo with {guid} not found.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("Todo is null");
        }

        [HttpDelete("{guid}")]
        public IActionResult DeleteTodo(Guid guid)
        {
            try
            {
                if (_todoService.DeleteTodo(guid))
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
