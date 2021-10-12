using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IRolesService
    {
        IEnumerable<RoleViewModel> GetAllRoles();
        Task<RoleViewModel> GetRoleByGuid(Guid guid);
        RoleViewModel CreateRole(RoleViewModel role);
        RoleViewModel UpdateRole(Guid guid, RoleViewModel role);
        Task<bool> DeleteRole(Guid guid);
    }

}