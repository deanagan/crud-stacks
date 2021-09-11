using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByGuid(Guid userGuid);
        User AddUser(User user);
        User UpdateUser(Guid userGuid, User user);
        bool DeleteUser(Guid userGuid);
    }
}
