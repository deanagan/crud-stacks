using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Dtos;

namespace TodoBackend.Api.Interfaces
{
    public interface IRolesRepository
    {
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<RoleDto> GetRoleByGuid(Guid guid);
        RoleDto AddRole(RoleDto parameter);
        RoleDto UpdateRole(Guid guid, RoleDto RoleDto);
        bool DeleteRole(Guid guid);
    }
}
