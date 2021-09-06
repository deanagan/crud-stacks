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

        public RoleDto AddRole(RoleDto roleDto)
        {
            var sql = @"
                        declare @Outcome table (
                            Id int,
                            UniqueId uniqueidentifier,
                            Created datetime,
                            Updated datetime
                        );
                        insert into dbo.Roles (UniqueId, Kind, Description)
                        output inserted.Id, inserted.UniqueId, inserted.Created, inserted.Updated into @Outcome
                        values (NEWID(), @Kind, @Description);

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
                parameter.Add("@Kind", roleDto.Kind);
                parameter.Add("@Description", roleDto.Description);

                conn.Execute(sql, parameter);

                var newRoleDto = roleDto.Clone();
                newRoleDto.Id = parameter.Get<int>("@Id");
                newRoleDto.UniqueId = parameter.Get<Guid>("@UniqueId");
                newRoleDto.Created = parameter.Get<DateTime>("@Created");
                newRoleDto.Updated = parameter.Get<DateTime>("@Updated");

                return newRoleDto;
            }
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