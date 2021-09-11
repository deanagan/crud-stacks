using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleByGuid(Guid guid);
        Role AddRole(Role parameter);
        Role UpdateRole(Guid guid, Role role);
        bool DeleteRole(Guid guid);
    }
}
