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

        [HttpGet("email-confirmation")]
        public async Task<IActionResult> EmailConfirmation(string token, string email)
        {
            try
            {
                var isConfirmed = await _authService.ConfirmEmail(token, email);
                if (isConfirmed)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { message = "Invalid confirmation credentials."});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

                var isRegisteredSuccessfully = await _authService.RegisterUser(registerView);

                if (!isRegisteredSuccessfully)
                {
                    return BadRequest();
                }

                var email = registerView.Email;
                var token = await _authService.RequestRegistrationToken(email);

                // TODO: Create  email confirmation
                // await _emailService.SendEmail(userTo, userFrom, subject, contentText, contentHtml);

                var emailConfirmation = new EmailTokenViewModel
                {
                    Email = registerView.Email,
                    Token = token
                };

                return Ok(emailConfirmation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

         [HttpPost("request-registration-token")]
        public async Task<IActionResult> RequestRegistrationToken(EmailViewModel tokenRequest)
        {
            try
            {
                var token = await _authService.RequestRegistrationToken(tokenRequest.Email);

                if (token == null)
                {
                    return BadRequest("Unable to request registration token");
                }

                var email = tokenRequest.Email;

                // TODO: Create  email confirmation
                // await _emailService.SendEmail(userTo, userFrom, subject, contentText, contentHtml);
                var emailConfirmation = new EmailTokenViewModel
                {
                    Email = email,
                    Token = token
                };

                // Return email token for now
                return Ok(emailConfirmation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("change-password/{guid}")]
        public async Task<IActionResult> ChangePassword(Guid guid, ChangePasswordViewModel changePasswordView)
        {
             try
            {

                var result = await _authService.ChangePassword(guid, changePasswordView);

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

        [HttpPost("request-reset-password")]
        public async Task<IActionResult> RequestResetPassword(EmailViewModel resetRequest)
        {
             try
            {
                var token = await _authService.RequestPasswordReset(resetRequest.Email);

                if (token == null)
                {
                    return BadRequest("Internal error when requesting password reset");
                }
                //TODO: Email to be sent
                var emailConfirmation = new EmailTokenViewModel
                {
                    Email = resetRequest.Email,
                    Token = token
                };
                // Return email token for now
                return Ok(emailConfirmation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordView)
        {
             try
            {
                var isReset = await _authService.ResetPassword(resetPasswordView);

                if (!isReset)
                {
                    return BadRequest("Password reset failed");
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
