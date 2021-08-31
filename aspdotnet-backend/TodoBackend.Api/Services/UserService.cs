using System.Collections.Generic;
using System.Linq;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Dtos;

using System.Threading.Tasks;
using AutoMapper;
using System;

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

        public UserView CreateUser(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            _userRepository.AddUser(userDto);
            return _mapper.Map<UserView>(userDto);
        }

        bool IUserService.DeleteUser(Guid guid)
        {
            throw new NotImplementedException();
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

        public bool UpdateUser(User user)
        {
            // var userDto = _mapper.Map<UserDto>(user);
            // _userRepository.AddUser(userDto);
            // return _mapper.Map<UserView>(userDto);
            return false;
        }
    }
}
