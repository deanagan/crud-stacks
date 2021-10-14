using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace TodoBackend.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        // TODO: Add email sender service
        //private readonly IEmailSender _emailSender;
        private readonly ILogger<UserService> _logger;

        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
                           RoleManager<Role> roleManager,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IMapper mapper,
                           ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserViewModel> CreateUser(UserViewModel userView)
        {
            var user = _mapper.Map<User>(userView);

            if (user.Role == null || user.Role.UniqueId == Guid.Empty)
            {
                var availableRoles = _roleManager.Roles;

                if (availableRoles == null)
                {
                    throw new Exception("There are no available roles registered and the user has not specified a role.");
                }

                user.Role = availableRoles.Where(roles => roles.Name == "Default").Select(r => r).FirstOrDefault();
            }

            var result = await _userManager.CreateAsync(user);
            if (result.Errors.Any())
            {
                throw new Exception($"The user creation failed: {result.Errors.First().Description}");
            }

            var role = await _roleManager.FindByIdAsync(user.Role.UniqueId.ToString());
            var newUser = await _userManager.FindByEmailAsync(user.Email);

            newUser.Role = new Role()
            {
                Id = role.Id,
                UniqueId = role.UniqueId,
                Name = role.Name,
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

        public IList<UserViewModel> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return _mapper.Map<IList<UserViewModel>>(users);
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
