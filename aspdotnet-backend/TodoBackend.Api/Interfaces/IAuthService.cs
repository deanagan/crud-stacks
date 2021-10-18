using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IAuthService
    {
        UserViewModel UpdatePassword(string hash, string newPassword, string oldPassword);
        Task<IdentityResult> RegisterUser(RegisterViewModel registerView);
        Task Logout();
        Task<AuthDataViewModel> Login(LoginViewModel loginView);
    }

}
