using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.ViewModels;


namespace TodoBackend.Api.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IAuthService authService, IUserService userService)
        {
            _logger = logger;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserView userView)
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterView registerView)
        {
            try
            {
                var users = await _userService.GetAllUsers();
                var userEmails = users.Select(u => u.Email);
                if (userEmails.Any(e => e == registerView.Email))
                {
                    return BadRequest(new { email = "User with this email already exists" });
                }

                var userView = _authService.RegisterUser(registerView);
                var newUser = await _userService.CreateUser(userView);

                return Ok(_authService.CreateAuthData(newUser.UniqueId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reset-password/{guid}")]
        public async Task<IActionResult> Reset(UserView guid, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
