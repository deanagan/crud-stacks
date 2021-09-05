using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface IRolesService
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleByGuid(Guid guid);
        Role CreateRole(Role role);
        Role UpdateRole(Guid guid, Role role);
        bool DeleteRole(Guid guid);
    }

}