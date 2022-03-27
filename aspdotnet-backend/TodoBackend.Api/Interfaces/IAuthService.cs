using System;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ChangePassword(Guid guid, ChangePasswordViewModel changePasswordView);
        Task<string> RequestPasswordReset(string email);
        Task<string> RequestRegistrationToken(string email);
        Task<bool> ResetPassword(ResetPasswordViewModel resetPasswordView);
        Task<bool> ConfirmEmail(string token, string email);
        Task<bool> RegisterUser(RegisterViewModel registerView);
        Task<AuthDataViewModel> Login(LoginViewModel loginView);
    }

}
