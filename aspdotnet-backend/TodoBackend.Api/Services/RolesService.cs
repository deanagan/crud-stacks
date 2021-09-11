using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;
using AutoMapper;


namespace TodoBackend.Api.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;
        public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public RoleView CreateRole(RoleView roleView)
        {
            var role = _mapper.Map<Role>(roleView);
            var newRole = _rolesRepository.AddRole(role);
            return _mapper.Map<RoleView>(newRole);
        }

        bool IRolesService.DeleteRole(Guid guid)
        {
            return _rolesRepository.DeleteRole(guid);
        }

        public async Task<IEnumerable<RoleView>> GetAllRoles()
        {
            var roles = await _rolesRepository.GetAllRoles();
            return _mapper.Map<IEnumerable<RoleView>>(roles);
        }

        public async Task<RoleView> GetRoleByGuid(Guid guid)
        {
            var role = await _rolesRepository.GetRoleByGuid(guid);
            return _mapper.Map<RoleView>(role);
        }

        public RoleView UpdateRole(Guid guid, RoleView roleView)
        {
            var role = _mapper.Map<Role>(roleView);
            var updatedRole = _rolesRepository.UpdateRole(guid, role);
            return _mapper.Map<RoleView>(updatedRole);
        }
    }
}
