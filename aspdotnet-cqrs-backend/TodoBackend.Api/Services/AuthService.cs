using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;

namespace TodoBackend.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly string _secretKey;
        private readonly int _tokenLifeSpan;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthService(IMapper mapper,
                           IConfiguration configuration,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _secretKey = configuration.GetValue<string>("Auth:JWTSecretKey");
            _tokenLifeSpan = configuration.GetValue<int>("Auth:JWTLifespan");
            _issuer = configuration.GetValue<string>("Auth:Issuer");
            _audience = configuration.GetValue<string>("Auth:Audience");

            if (_secretKey == null || _tokenLifeSpan == 0)
            {
                throw new Exception("Secrets have not been setup!");
            }
        }

        public async Task<IdentityResult> RegisterUser(RegisterViewModel registerView)
        {
            var userView = _mapper.Map<UserViewModel>(registerView);
            var user = _mapper.Map<User>(userView);

            var availableRoles = _roleManager.Roles;
            if (availableRoles == null)
            {
                throw new Exception("There are no available roles registered and the user has not specified a role.");
            }

            if (user.Role == null || user.Role.UniqueId == Guid.Empty)
            {
                user.Role = availableRoles.Where(roles => roles.Name == "Default").FirstOrDefault();
            }
            else
            {
                user.Role = availableRoles.Where(ar => ar.UniqueId == user.Role.UniqueId).FirstOrDefault();
            }

            var result = await _userManager.CreateAsync(user, registerView.Password);
            if (result.Succeeded)
            {
                var registeredUser = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(registeredUser, registeredUser.Role.Name);
            }

            return result;
        }

        public async Task<string> RequestRegistrationToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }
            var tokenToEncode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return HttpUtility.UrlEncode(tokenToEncode);
        }

        private string GenerateToken(User user)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenLifeSpan),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            var key = Encoding.ASCII.GetBytes(_secretKey);


            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private AuthDataViewModel CreateAuthData(User user)
        {
            var token = GenerateToken(user);

            return new AuthDataViewModel()
            {
                Email = user.Email,
                Role = user.Role.Name,
                UserName = user.UserName,
                Token = token,
                TokenExpirationTime = _tokenLifeSpan
            };
        }

        public async Task<AuthDataViewModel> Login(LoginViewModel loginView)
        {
            var user = await _userManager.FindByEmailAsync(loginView.Email);
            var isValidUser = user != null && await _userManager.CheckPasswordAsync(user, loginView.Password);
            isValidUser = isValidUser && await _userManager.IsEmailConfirmedAsync(user);

            return isValidUser ? CreateAuthData(user) : null;
        }

        public async Task<bool> ChangePassword(Guid guid, ChangePasswordViewModel changePasswordView)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordView.Email);
            if (user == null)
            {
                return false;
            }

            var changeResult = await _userManager.ChangePasswordAsync(user, changePasswordView.OldPassword, changePasswordView.NewPassword);

            return changeResult.Succeeded;
        }

        public async Task<bool> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<string> RequestPasswordReset(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return HttpUtility.UrlEncode(token);
        }

        public async Task<bool> ResetPassword(ResetPasswordViewModel resetPasswordView)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordView.Email);

            if (user == null)
            {
                return false;
            }

            var isConfirmedUser = await _userManager.IsEmailConfirmedAsync(user);
            if (!isConfirmedUser)
            {
                return false;
            }

            var decodedToken = HttpUtility.UrlDecode(resetPasswordView.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordView.NewPassword);

            return result.Succeeded;
        }
    }
}
