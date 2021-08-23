using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using PlanningBoard.Api.Interfaces;
using PlanningBoard.Api.Data.Models;


namespace PlanningBoard.Api.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await _userService.GetUsersAsync();
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
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var result = await _userService.GetUser(id);
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
        public IActionResult CreateUser(User user)
        {
            if (user != null)
            {
                try
                {
                    _userService.CreateUser(user);
                    return CreatedAtAction(nameof(GetUsers), new { Id = user.Id }, user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("user creation failed.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (user != null)
            {
                try
                {
                    user.Id = id;
                    if (_userService.UpdateUser(user))
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest($"User with {id} not found.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("User is null");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (_userService.DeleteUser(id))
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
