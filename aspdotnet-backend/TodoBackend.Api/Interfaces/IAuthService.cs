using System;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IAuthService
    {
        UserViewModel UpdatePassword(string hash, string newPassword, string oldPassword);
        Task<bool> RegisterUser(RegisterViewModel registerView);
        bool VerifyPassword(string hash, string password);
        AuthDataViewModel CreateAuthData(Guid guid);
    }

}
