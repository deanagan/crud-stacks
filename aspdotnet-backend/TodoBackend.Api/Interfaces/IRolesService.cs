using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IRolesService
    {
        Task<IEnumerable<RoleView>> GetAllRoles();
        Task<RoleView> GetRoleByGuid(Guid guid);
        RoleView CreateRole(RoleView role);
        RoleView UpdateRole(Guid guid, RoleView role);
        bool DeleteRole(Guid guid);
    }

}