using System;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IAuthService
    {
        UserView UpdatePassword(string hash, string newPassword, string oldPassword);
        bool VerifyPassword(string hash, string password);
        UserView RegisterUser(RegisterView registerView);
        AuthDataView CreateAuthData(Guid guid);
    }

}
