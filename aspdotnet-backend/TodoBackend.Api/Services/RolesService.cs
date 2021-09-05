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
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;
        public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        Role IRolesService.CreateRole(Role role)
        {
            throw new NotImplementedException();
        }

        bool IRolesService.DeleteRole(Guid guid)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
