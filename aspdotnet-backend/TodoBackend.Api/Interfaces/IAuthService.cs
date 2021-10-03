using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IAuthService
    {
        UserView UpdatePassword(UserView user, string newPassword, string oldPassword);
        bool VerifyPassword(UserView user, string password);
    }

}
