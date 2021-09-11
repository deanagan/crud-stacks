using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserView>> GetAllUsers();
        Task<UserView> GetUserByGuid(Guid guid);
        UserView CreateUser(UserView user);
        UserView UpdateUser(Guid guid, UserView user);
        bool DeleteUser(Guid guid);
    }

}
