using System.Collections.Generic;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Dtos;

using System.Threading.Tasks;
using AutoMapper;
using System;

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

        public Role CreateRole(Role role)
        {
            var roleDto = _mapper.Map<RoleDto>(role);
            var newRole = _rolesRepository.AddRole(roleDto);
            return _mapper.Map<Role>(newRole);
        }

        bool IRolesService.DeleteRole(Guid guid)
        {
            return _rolesRepository.DeleteRole(guid);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var rolesDto = await _rolesRepository.GetAllRoles();
            return _mapper.Map<IEnumerable<Role>>(rolesDto);
        }

        public async Task<Role> GetRoleByGuid(Guid guid)
        {
            var roleDto = await _rolesRepository.GetRoleByGuid(guid);
            return _mapper.Map<Role>(roleDto);
        }

        Role IRolesService.UpdateRole(Guid guid, Role role)
        {
            var roleDto = _mapper.Map<RoleDto>(role);
            var newRoleDto = _rolesRepository.UpdateRole(guid, roleDto);
            return _mapper.Map<Role>(newRoleDto);
        }
    }
}
