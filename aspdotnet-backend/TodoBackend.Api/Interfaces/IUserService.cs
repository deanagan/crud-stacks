using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserView>> GetAllUsers();
        Task<UserView> GetUserByGuid(Guid guid);
        Task<IEnumerable<UserView>> GetUsersByGuids(IEnumerable<Guid> guids);
        UserView CreateUser(UserView user);
        UserView UpdateUser(Guid guid, UserView user);
        bool DeleteUser(Guid guid);
    }

}
