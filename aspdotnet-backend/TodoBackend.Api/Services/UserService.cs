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
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserService> _logger;

        private readonly IMapper _mapper;
        public UserService(RoleManager<Role> roleManager,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IMapper mapper,
                           ILogger<UserService> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserViewModel> CreateUser(UserViewModel userView)
        {
            var user = _mapper.Map<User>(userView);

            user.Role = CreateRole(user);

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

        private Role CreateRole(User user)
        {
            var availableRoles = _roleManager.Roles;
            if (availableRoles == null)
            {
                throw new Exception("There are no available roles registered and the user has not specified a role.");
            }

            if (user.Role == null || user.Role.UniqueId == Guid.Empty)
            {
                return availableRoles.Where(roles => roles.Name == "Default").FirstOrDefault();
            }

            return availableRoles.Where(ar => ar.UniqueId == user.Role.UniqueId).FirstOrDefault();
        }

        public async Task<bool> DeleteUser(Guid guid)
        {
            var result = await _userManager.DeleteAsync(new User { UniqueId = guid});

            return result.Succeeded;
        }

        public IList<UserViewModel> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return _mapper.Map<IList<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetUserByGuid(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> UpdateUser(Guid guid, UserViewModel userView)
        {
            var user = _mapper.Map<User>(userView);
            user.UniqueId = guid;
            user.Role = CreateRole(user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Errors.Any())
            {
                throw new Exception($"The user update failed: {result.Errors.First().Description}");
            }

             var role = await _roleManager.FindByIdAsync(user.Role.UniqueId.ToString());
            var updatedUser = await _userManager.FindByEmailAsync(user.Email);

            updatedUser.Role = new Role()
            {
                Id = role.Id,
                UniqueId = role.UniqueId,
                Name = role.Name,
                Description = role.Description,
                Created = role.Created,
                Updated = role.Updated
            };
            return _mapper.Map<UserViewModel>(updatedUser);

        }
    }
}
