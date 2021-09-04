using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Dtos;

namespace TodoBackend.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserByGuid(Guid userGuid);
        UserDto AddUser(UserDto parameter);
        UserDto UpdateUser(Guid userGuid, UserDto userDto);
        bool DeleteUser(Guid userGuid);
    }
}
