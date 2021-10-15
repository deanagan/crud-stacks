using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserService
    {
        IList<UserViewModel> GetAllUsers();
        Task<UserViewModel> GetUserByGuid(Guid guid);
        Task<UserViewModel> CreateUser(UserViewModel user);
        Task<UserViewModel> UpdateUser(Guid guid, UserViewModel userView);
        Task<bool> DeleteUser(Guid guid);
    }

}
