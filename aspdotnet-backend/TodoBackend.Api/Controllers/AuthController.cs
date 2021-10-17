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
        public IActionResult Login(LoginViewModel loginView)
        {
            try
            {
                var users = _userService.GetAllUsers();
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

                // var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                // var result = await _userManager.CreateAsync(user, model.Password);
                // if (result.Succeeded)
                // {
                //     _logger.LogInformation("User created a new account with password.");

                //     var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //     var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //     await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                //     await _signInManager.SignInAsync(user, isPersistent: false);
                //     _logger.LogInformation("User created a new account with password.");
                //     return RedirectToLocal(returnUrl);
                // }
                // AddErrors(result);

                var result = await _authService.RegisterUser(registerView);

                if (!result)
                {
                    return BadRequest("Failed to register user");
                }

                // TODO: Create user

                return Ok(result);
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
