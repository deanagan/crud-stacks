using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Models;


namespace TodoBackend.Api.Controllers
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
                var result = await _userService.GetAllUsers();
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
        public async Task<IActionResult> GetUser(Guid guid)
        {
            try
            {
                var result = await _userService.GetUserByGuid(guid);
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
                    var newUser = _userService.CreateUser(user);
                    return CreatedAtAction(nameof(GetUsers), new { UniqueId = newUser.UniqueId }, newUser);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("user creation failed.");
        }

        [HttpPut("{guid}")]
        public IActionResult UpdateUser(Guid guid, User user)
        {
            if (user != null)
            {
                try
                {
                    var updatedUser = _userService.UpdateUser(guid, user);
                    if (updatedUser != null)
                    {
                        return Ok(updatedUser);
                    }
                    else
                    {
                        return BadRequest($"User with {guid} not found.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("User is null");
        }

        [HttpDelete("{guid}")]
        public IActionResult DeleteUser(Guid guid)
        {
            try
            {
                if (_userService.DeleteUser(guid))
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
