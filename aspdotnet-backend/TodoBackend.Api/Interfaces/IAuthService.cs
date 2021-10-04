using System;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IAuthService
    {
        UserView UpdatePassword(UserView userView, string newPassword, string oldPassword);
        bool VerifyPassword(UserView userView, string password);
        UserView RegisterUser(RegisterView registerView);
        AuthDataView CreateAuthData(Guid guid);
    }

}
