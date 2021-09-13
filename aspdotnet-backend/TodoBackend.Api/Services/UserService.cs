using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;


namespace TodoBackend.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _roleRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IRolesRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<UserView> CreateUser(UserView userView)
        {
            var user = _mapper.Map<User>(userView);
            var newUser = _userRepository.AddUser(user);
            var role = await _roleRepository.GetRoleByGuid(user.Role.UniqueId);

            newUser.Role = new Role()
            {
                Id = role.Id,
                UniqueId = role.UniqueId,
                Kind = role.Kind,
                Description = role.Description,
                Created = role.Created,
                Updated = role.Updated
            };
            return _mapper.Map<UserView>(newUser);
        }

        public bool DeleteUser(Guid guid)
        {
            return _userRepository.DeleteUser(guid);
        }

        public async Task<IEnumerable<UserView>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserView>>(users);
        }

        public async Task<UserView> GetUserByGuid(Guid guid)
        {
            var user = await _userRepository.GetUserByGuid(guid);
            return _mapper.Map<UserView>(user);
        }

        public UserView UpdateUser(Guid guid, UserView userView)
        {
            var user = _mapper.Map<User>(userView);
            var updatedUser = _userRepository.UpdateUser(guid, user);
            return _mapper.Map<UserView>(updatedUser);
        }

        public async Task<IEnumerable<UserView>> GetUsersByGuids(IEnumerable<Guid> guids)
        {
            var users = await _userRepository.GetUsersByGuids(guids);
            return _mapper.Map<IEnumerable<UserView>>(users);
        }
    }
}
