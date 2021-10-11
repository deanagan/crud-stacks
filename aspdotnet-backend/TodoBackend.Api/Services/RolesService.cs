using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace TodoBackend.Api.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RolesService(RoleManager<Role> roleManager, IRolesRepository rolesRepository, IMapper mapper)
        {
            _roleManager = roleManager;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public RoleViewModel CreateRole(RoleViewModel roleView)
        {
            var role = _mapper.Map<Role>(roleView);
            role.NormalizedName = role.Name.ToUpper();
            var newRole = _rolesRepository.AddRole(role);
            return _mapper.Map<RoleViewModel>(newRole);
        }

        public async Task<bool> DeleteRole(Guid guid)
        {
            var result = await _roleManager.DeleteAsync(new Role { UniqueId = guid });
            return result.Succeeded;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRoles()
        {
            var roles = await _rolesRepository.GetAllRoles();
            return _mapper.Map<IEnumerable<RoleViewModel>>(roles);
        }

        public async Task<RoleViewModel> GetRoleByGuid(Guid guid)
        {
            var role = await _rolesRepository.GetRoleByGuid(guid);
            return _mapper.Map<RoleViewModel>(role);
        }

        public RoleViewModel UpdateRole(Guid guid, RoleViewModel roleView)
        {
            var role = _mapper.Map<Role>(roleView);
            var updatedRole = _rolesRepository.UpdateRole(guid, role);
            return _mapper.Map<RoleViewModel>(updatedRole);
        }
    }
}
