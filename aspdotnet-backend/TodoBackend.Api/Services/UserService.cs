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

        void IUserService.CreateUser(UserView user)
        {
            throw new NotImplementedException();
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

        Task<UserView> IUserService.GetUserByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        bool IUserService.UpdateUser(UserView user)
        {
            throw new NotImplementedException();
        }
    }
}
