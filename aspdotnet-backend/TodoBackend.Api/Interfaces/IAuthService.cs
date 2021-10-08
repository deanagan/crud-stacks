using System;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IAuthService
    {
        UserViewModel UpdatePassword(string hash, string newPassword, string oldPassword);
        bool VerifyPassword(string hash, string password);
        UserViewModel RegisterUser(RegisterViewModel registerView);
        AuthDataViewModel CreateAuthData(Guid guid);
    }

}
