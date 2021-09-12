using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<User>> GetUsersByGuids(IEnumerable<Guid> guids);
        Task<User> GetUserByGuid(Guid guid);
        User AddUser(User user);
        User UpdateUser(Guid guid, User user);
        bool DeleteUser(Guid guid);
    }
}
