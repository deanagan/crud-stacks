using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace TodoBackend.Api.Data.Identity
{
    public class RolesRepository : IQueryableRoleStore<Role>
    {
        private readonly string _connectionString;

        public IQueryable<Role> Roles { get { return GetAllRoles(); } }

        public RolesRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        private IQueryable<Role> GetAllRoles()
        {
            var sql = @"
                    select r.Id,
                           r.UniqueId,
                           r.Name,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Roles as r with (nolock)";

            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<Role>(sql).AsQueryable();
            }
        }

        private async Task<Role> GetRoleByGuid(Guid guid)
        {
            var sql = @"
                    select r.Id,
                           r.UniqueId,
                           r.Name,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Roles as r with (nolock)
                        where r.UniqueId = @UniqueId";

            using (var conn = new SqlConnection(_connectionString))
            {
                var Role = await conn.QueryAsync<Role>(sql, new { UniqueId = guid });
                return Role.FirstOrDefault();
            }
        }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var sql = @"insert into dbo.Roles (UniqueId, Name, NormalizedName, Description)
                        values (NEWID(), @Name, @NormalizedName, @Description);
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", role.Name);
                parameter.Add("@NormalizedName", role.NormalizedName);
                parameter.Add("@Description", role.Description);
                conn.Execute(sql, parameter);
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sql = @"update r
                        set r.Name = coalesce(@Name, r.Name),
                            r.NormalizedName = UPPER(coalesce(@Name, r.Name)),
                            r.Description = coalesce(@Description, r.Description),
                            r.Updated = getutcdate()
                        from dbo.Roles r
                        where r.UniqueId = @UniqueId
                        ";


            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", role.UniqueId);
                parameter.Add("@Name", role.Name);
                parameter.Add("@Description", role.Description);

                conn.Execute(sql, parameter);
            }
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sql = @"
                        delete r
                        from dbo.Roles r
                        where r.UniqueId = @UniqueId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", role.UniqueId);

                conn.Execute(sql, parameter);
            }
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
             return Task.FromResult(role.UniqueId.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sql = @"
                    select r.Id,
                           r.UniqueId,
                           r.Name,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Roles as r with (nolock)
                        where r.UniqueId = @UniqueId";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QuerySingleOrDefaultAsync<Role>(sql, new { UniqueId = roleId });
            }
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sql = @"
                    select r.Id,
                           r.UniqueId,
                           r.Name,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Roles as r with (nolock)
                        where r.NormalizedName = @NormalizedName";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QuerySingleOrDefaultAsync<Role>(sql, new { NormalizedName = normalizedRoleName });
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            // Nothing to dispose
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}