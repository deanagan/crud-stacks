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
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IAuthService authService, IUserService userService, IEmailService emailService)
        {
            _logger = logger;
            _authService = authService;
            _userService = userService;
            _emailService = emailService;
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

                var (result, token) = await _authService.RegisterUser(registerView);

                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(err => ModelState.AddModelError(err.Code, err.Description));
                    return BadRequest(ModelState);
                }

                // TODO: Create  email confirmation
                // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // await _emailService.SendEmail(userTo, userFrom, subject, contentText, contentHtml);

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
