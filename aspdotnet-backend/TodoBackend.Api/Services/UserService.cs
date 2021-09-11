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
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserView CreateUser(UserView userView)
        {
            var user = _mapper.Map<User>(userView);
            var newUser = _userRepository.AddUser(user);
            return _mapper.Map<UserView>(newUser);
        }

        public bool DeleteUser(Guid guid)
        {
            return _userRepository.DeleteUser(guid);
        }

        public async Task<IEnumerable<UserView>> GetAllUsers()
        {
            var userDtos = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserView>>(userDtos);
        }

        public async Task<UserView> GetUserByGuid(Guid guid)
        {
            var userDtos = await _userRepository.GetUserByGuid(guid);
            return _mapper.Map<UserView>(userDtos);
        }

        public UserView UpdateUser(Guid guid, UserView userView)
        {
            var user = _mapper.Map<User>(userView);
            var newUserDto = _userRepository.UpdateUser(guid, user);
            return _mapper.Map<UserView>(newUserDto);
        }
    }
}
