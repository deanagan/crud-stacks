using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IAuthService
    {
        Task<bool> UpdatePassword(Guid guid, ChangePasswordViewModel changePasswordView);
        Task<IdentityResult> RegisterUser(RegisterViewModel registerView);
        Task<AuthDataViewModel> Login(LoginViewModel loginView);
    }

}
