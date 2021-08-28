using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserView>> GetAllUsers();
        Task<UserView> GetUserByGuid(Guid guid);
        void CreateUser(UserView user);
        bool UpdateUser(UserView user);
        bool DeleteUser(Guid guid);
    }

}
