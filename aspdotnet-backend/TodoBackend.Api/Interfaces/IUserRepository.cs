using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Dtos;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserByGuid(Guid user);
        void AddUser(UserDto parameter);
        void AddUsers(IEnumerable<UserDto> userGuids);
        void UpdateUser(Guid userGuid);
        void DeleteUser(Guid userGuid);
    }
}
