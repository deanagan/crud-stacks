using System;
using System.Linq;
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

        public async Task<UserViewModel> CreateUser(UserViewModel userView)
        {
            var user = _mapper.Map<User>(userView);

            if (user.Role == null)
            {
                var availableRoles = await _roleRepository.GetAllRoles();

                if (availableRoles == null)
                {
                    throw new FormatException("There are no available roles registered and the user has not specified a role.");
                }

                user.Role = availableRoles.Where(roles => roles.Kind == "Default").Select(r => r).FirstOrDefault();
            }
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
            return _mapper.Map<UserViewModel>(newUser);
        }

        public bool DeleteUser(Guid guid)
        {
            return _userRepository.DeleteUser(guid);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetUserByGuid(Guid guid)
        {
            var user = await _userRepository.GetUserByGuid(guid);
            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel UpdateUser(Guid guid, UserViewModel userView)
        {
            var user = _mapper.Map<User>(userView);
            var updatedUser = _userRepository.UpdateUser(guid, user);
            return _mapper.Map<UserViewModel>(updatedUser);
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersByGuids(IEnumerable<Guid> guids)
        {
            var users = await _userRepository.GetUsersByGuids(guids);
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }
    }
}
