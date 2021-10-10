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
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            try
            {
                var users = await _userService.GetAllUsers();
                var user = users.Where(ue => ue.Email == loginView.Email).Select(u => u).FirstOrDefault();

                if (user == null)
                {
                    return BadRequest(new { message = "User with this email does not exist" });
                }


                if (_authService.VerifyPassword(user.PasswordHash, loginView.Password))
                {
                    return Ok(_authService.CreateAuthData(user.UniqueId));
                }
                else
                {
                    return BadRequest(new { message = "Invalid login credentials."});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerView)
        {
            try
            {
                var users = await _userService.GetAllUsers();
                var userEmails = users.Select(u => u.Email);
                if (userEmails.Any(e => e == registerView.Email))
                {
                    return BadRequest(new { message = "User with this email already exists" });
                }

                var userView = _authService.RegisterUser(registerView);
                if (userView == null)
                {
                    return BadRequest(new { message = "Registration failed" });
                }

                var newUser = await _userService.CreateUser(userView);
                if (newUser == null)
                {
                    return BadRequest(new { message = "User creation failed" });
                }

                // Add user role
                //switch()



                return Ok(_authService.CreateAuthData(newUser.UniqueId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reset-password/{guid}")]
        public async Task<IActionResult> Reset(UserViewModel guid, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
