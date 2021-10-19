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
                var authData = await _authService.Login(loginView);
                if (authData != null)
                {
                    return Ok(authData);
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

                var result = await _authService.RegisterUser(registerView);

                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(err => ModelState.AddModelError(err.Code, err.Description));
                    return BadRequest(ModelState);
                }

                // TODO: Create user or email confirmation
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password/{guid}")]
        public async Task<IActionResult> Reset(Guid guid, ChangePasswordViewModel changePasswordView)
        {
             try
            {

                var result = await _authService.UpdatePassword(guid, changePasswordView);

                if (!result)
                {
                    return BadRequest("Internal error when resetting password");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
