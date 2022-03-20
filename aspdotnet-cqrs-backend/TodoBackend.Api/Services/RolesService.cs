using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace TodoBackend.Api.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RolesService(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleViewModel> CreateRole(RoleViewModel roleView)
        {
            var role = _mapper.Map<Role>(roleView);
            role.NormalizedName = role.Name.ToUpper();
            var result = await _roleManager.CreateAsync(role);
            if (result.Errors.Any())
            {
                throw new Exception($"Failed to create role '{role.Name}'.");
            }

            var newRole = await _roleManager.FindByNameAsync(role.NormalizedName);
            return _mapper.Map<RoleViewModel>(newRole);
        }

        public async Task<bool> DeleteRole(Guid guid)
        {
            var result = await _roleManager.DeleteAsync(new Role { UniqueId = guid });
            return result.Succeeded;
        }

        public IEnumerable<RoleViewModel> GetAllRoles()
        {
            var roles = _roleManager.Roles;
            return _mapper.Map<IEnumerable<RoleViewModel>>(roles);
        }

        public async Task<RoleViewModel> GetRoleByGuid(Guid guid)
        {
            var role = await _roleManager.FindByIdAsync(guid.ToString());
            return _mapper.Map<RoleViewModel>(role);
        }

        public async Task<RoleViewModel> UpdateRole(Guid guid, RoleViewModel roleView)
        {
            var role = _mapper.Map<Role>(roleView);
            role.UniqueId = guid;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Errors.Any())
            {
                throw new Exception($"Failed to update role '{role.Name}'.");
            }
            var updatedRole = await _roleManager.FindByIdAsync(guid.ToString());
            return _mapper.Map<RoleViewModel>(updatedRole);
        }
    }
}
