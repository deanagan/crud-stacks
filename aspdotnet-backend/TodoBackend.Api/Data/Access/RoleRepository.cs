using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.Extensions.Configuration;

using Dapper;

using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Dtos;


namespace TodoBackend.Api.Data.Access
{
    public class RolesRepository : IRolesRepository
    {
        private readonly string _connectionString;
        public RolesRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
             var sql = @"
                    select r.Id,
                           r.UniqueId,
                           r.Kind,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Roles as r with (nolock)";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<RoleDto>(sql);
            }
        }

        public async Task<RoleDto> GetRoleByGuid(Guid guid)
        {
            var sql = @"
                    select r.Id,
                           r.UniqueId,
                           r.Kind,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Roles as r with (nolock)
                        where r.UniqueId = @UniqueId";

            using (var conn = new SqlConnection(_connectionString))
            {
                var roleDto = await conn.QueryAsync<RoleDto>(sql, new { UniqueId = guid });
                return roleDto.FirstOrDefault();
            }
        }

        RoleDto IRolesRepository.AddRole(RoleDto parameter)
        {
            throw new NotImplementedException();
        }

        RoleDto IRolesRepository.UpdateRole(Guid guid, RoleDto RoleDto)
        {
            throw new NotImplementedException();
        }

        bool IRolesRepository.DeleteRole(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}