using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsers();
        Task<UserViewModel> GetUserByGuid(Guid guid);
        Task<IEnumerable<UserViewModel>> GetUsersByGuids(IEnumerable<Guid> guids);
        Task<UserViewModel> CreateUser(UserViewModel user);
        UserViewModel UpdateUser(Guid guid, UserViewModel user);
        bool DeleteUser(Guid guid);
    }

}
