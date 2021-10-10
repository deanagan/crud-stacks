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

namespace TodoBackend.Api.Data.Access
{
    public class RolesRepository : IRolesRepository, IRoleStore<Role>
    {
        private readonly string _connectionString;
        private bool _disposedValue;

        public RolesRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
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
                return await conn.QueryAsync<Role>(sql);
            }
        }

        public async Task<Role> GetRoleByGuid(Guid guid)
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

        public Role AddRole(Role role)
        {
            var sql = @"
                        declare @Outcome table (
                            Id int,
                            UniqueId uniqueidentifier,
                            Created datetime,
                            Updated datetime
                        );
                        insert into dbo.Roles (UniqueId, Name, NormalizedName, Description)
                        output inserted.Id, inserted.UniqueId, inserted.Created, inserted.Updated into @Outcome
                        values (NEWID(), @Name, @NormalizedName, @Description);

                        select @Id = Id,
                               @UniqueId = UniqueId,
                               @Created = Created,
                               @Updated = Updated
                        from @Outcome;
                        ";


            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@UniqueId", null, DbType.Guid, ParameterDirection.Output);
                parameter.Add("@Created", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@Updated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@Name", role.Name);
                parameter.Add("@NormalizedName", role.NormalizedName);
                parameter.Add("@Description", role.Description);

                conn.Execute(sql, parameter);

                var newRole = new Role()
                {
                    Id = parameter.Get<int>("@Id"),
                    UniqueId = parameter.Get<Guid>("@UniqueId"),
                    Name = role.Name,
                    Description = role.Description,
                    Created = parameter.Get<DateTime>("@Created"),
                    Updated = parameter.Get<DateTime>("@Updated")
                };

                return newRole;
            }
        }

        Role IRolesRepository.UpdateRole(Guid guid, Role role)
        {
            var sql = @"
                        declare @Outcome table (
                            Id int,
                            Name nvarchar(256),
                            Description nvarchar(max),
                            Created datetime,
                            Updated datetime
                        );

                        update r
                        set r.Name = coalesce(@Name, r.Name),
                            r.NormalizedName = UPPER(coalesce(@Name, r.Name)),
                            r.Description = coalesce(@Description, r.Description),
                            r.Updated = getutcdate()
                        output inserted.Id,
                               inserted.Name,
                               inserted.Description,
                               inserted.Created,
                               inserted.Updated
                        into @Outcome
                        from dbo.Roles r
                        where r.UniqueId = @UniqueId

                        select @Id = Id,
                               @Created = Created,
                               @Updated = Updated,
                               @NewName = Name,
                               @NewDescription = Description
                        from @Outcome;
                        ";


            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", null, DbType.Int32, ParameterDirection.Output);
                parameter.Add("@Created", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@Updated", null, DbType.DateTime, ParameterDirection.Output);
                parameter.Add("@NewName", null, DbType.String, ParameterDirection.Output, 256);
                parameter.Add("@NewDescription", null, DbType.String, ParameterDirection.Output, -1);

                parameter.Add("@UniqueId", guid);
                parameter.Add("@Name", role.Name);
                parameter.Add("@Description", role.Description);

                conn.Execute(sql, parameter);

                var updatedRole = new Role()
                {
                    Id = parameter.Get<int>("@Id"),
                    UniqueId = guid,
                    Name = parameter.Get<string>("@NewName"),
                    Description = parameter.Get<string>("@NewDescription"),
                    Created = parameter.Get<DateTime>("@Created"),
                    Updated = parameter.Get<DateTime>("@Updated")
                };

                return updatedRole;
            }
        }

        public bool DeleteRole(Guid guid)
        {
            var sql = @"
                        delete r
                        from dbo.Roles r
                        where r.UniqueId = @UniqueId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", guid);

                var s = conn.Execute(sql, parameter) != 0;
                return s;
            }
        }

        Task<IdentityResult> IRoleStore<Role>.CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            AddRole(role);
            return Task.FromResult(IdentityResult.Success);
        }

        Task<IdentityResult> IRoleStore<Role>.UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IdentityResult> IRoleStore<Role>.DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<string> IRoleStore<Role>.GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<string> IRoleStore<Role>.GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IRoleStore<Role>.SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<string> IRoleStore<Role>.GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IRoleStore<Role>.SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Role> IRoleStore<Role>.FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Role> IRoleStore<Role>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RolesRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}