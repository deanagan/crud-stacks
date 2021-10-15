using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Dapper;
using TodoBackend.Api.Data.Models;


namespace TodoBackend.Api.Data.Identity
{
    public class UserRepository : IQueryableUserStore<User>,
                                  IUserEmailStore<User>,
                                  IUserPhoneNumberStore<User>,
                                  IUserTwoFactorStore<User>,
                                  IUserPasswordStore<User>,
                                  IUserRoleStore<User>
    {
        private readonly string _connectionString;

        public IQueryable<User> Users {
            get {
                var sql = @"
                    select u.Id,
                           u.UniqueId,
                           u.UserName,
                           u.NormalizedUserName,
                           u.FirstName,
                           u.LastName,
                           u.Email,
                           u.NormalizedEmail,
                           u.PasswordHash,
                           u.Created,
                           u.Updated,
                           r.Id,
                           r.UniqueId,
                           r.Name,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleUniqueId = r.UniqueId
                    where u.IsDeleted = 0";

            using (var conn = new SqlConnection(_connectionString))
            {
                var users = conn.Query<User, Role, User>(sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                });

                return users.AsQueryable();
            }

            }
        }

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public User AddUser(User user)
        {
            var sql = @"insert into dbo.Users (
                            [UniqueId],
                            [FirstName],
                            [LastName],
                            [UserName],
                            [NormalizedUserName],
                            [Email],
                            [NormalizedEmail],
                            [EmailConfirmed],
                            [PhoneNumberConfirmed],
                            [TwoFactorEnabled],
                            [PasswordHash],
                            [RoleUniqueId]
                        )
                        values (
                            NEWID(),
                            @FirstName,
                            @LastName,
                            @UserName,
                            upper(@UserName),
                            @Email,
                            upper(@Email),
                            0,
                            0,
                            0,
                            @PasswordHash,
                            @RoleUniqueId
                            );
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@FirstName", user.FirstName);
                parameter.Add("@LastName", user.LastName);
                parameter.Add("@Email", user.Email);
                parameter.Add("@PasswordHash", user.PasswordHash);
                parameter.Add("@RoleUniqueId", user.Role.UniqueId);
                parameter.Add("@UserName", user.UserName);

                var result = conn.Execute(sql, parameter);

                var newUser = new User()
                {
                    Id = parameter.Get<int>("@Id"),
                    UniqueId = parameter.Get<Guid>("@UniqueId"),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    Created = parameter.Get<DateTime>("@UserCreated"),
                    Updated = parameter.Get<DateTime>("@UserUpdated"),
                };

                return newUser;
            }
        }

        public async Task<IEnumerable<User>> GetUsersByGuids(IEnumerable<Guid> guids)
        {
            var sql = @"
                    select u.Id,
                           u.UniqueId,
                           u.FirstName,
                           u.LastName,
                           u.Email,
                           u.PasswordHash,
                           u.Created,
                           u.Updated,
                           r.Id,
                           r.UniqueId,
                           r.Name,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleUniqueId = r.UniqueId
                    where u.UniqueId in @UniqueIds and u.IsDeleted = 0";

            using (var conn = new SqlConnection(_connectionString))
            {
                var users = await conn.QueryAsync<User, Role, User>(sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                }, new { UniqueIds = guids });

                return users;
            }
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UniqueId.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sql = @"insert into dbo.Users (
                            [UniqueId],
                            [FirstName],
                            [LastName],
                            [UserName],
                            [NormalizedUserName],
                            [Email],
                            [NormalizedEmail],
                            [EmailConfirmed],
                            [PhoneNumberConfirmed],
                            [TwoFactorEnabled],
                            [PasswordHash],
                            [RoleUniqueId]
                        )
                        values (
                            NEWID(),
                            @FirstName,
                            @LastName,
                            @UserName,
                            upper(@UserName),
                            @Email,
                            upper(@Email),
                            0,
                            0,
                            0,
                            @PasswordHash,
                            @RoleUniqueId
                            );
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@FirstName", user.FirstName);
                parameter.Add("@LastName", user.LastName);
                parameter.Add("@Email", user.Email);
                parameter.Add("@PasswordHash", user.PasswordHash);
                parameter.Add("@RoleUniqueId", user.Role.UniqueId);
                parameter.Add("@UserName", user.UserName);
                await conn.OpenAsync(cancellationToken);
                await conn.ExecuteAsync(sql, parameter);
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var sql = @"
                        update u
                        set u.UserName = @UserName,
                            u.NormalizedUserName = @NormalizedUserName,
                            u.FirstName = @FirstName,
                            u.LastName = @LastName,
                            u.Email = @Email,
                            u.NormalizedEmail = @NormalizedEmail,
                            u.EmailConfirmed = @EmailConfirmed,
                            u.PasswordHash = @PasswordHash,
                            u.Updated = getutcdate(),
                            u.RoleUniqueId = @RoleUniqueId
						from dbo.Users u
                        where u.UniqueId = @UniqueId and u.IsDeleted = 0
                            ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", user.UniqueId);
                parameter.Add("@UserName", user.UserName);
                parameter.Add("@NormalizedUserName", user.NormalizedUserName);
                parameter.Add("@FirstName", user.FirstName);
                parameter.Add("@LastName", user.LastName);
                parameter.Add("@Email", user.Email);
                parameter.Add("@NormalizedEmail", user.NormalizedEmail);
                parameter.Add("@EmailConfirmed", user.EmailConfirmed);
                parameter.Add("@PasswordHash", user.PasswordHash);
                parameter.Add("@RoleUniqueId", user.Role.UniqueId);

                await conn.OpenAsync(cancellationToken);
                await conn.ExecuteAsync(sql, parameter);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sql = @"
						update u
                        set u.IsDeleted = 1
                        from dbo.Users u
                        where u.UniqueId = @UniqueId;
                        ";

            using (var conn = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UniqueId", user.UniqueId);
                await conn.OpenAsync(cancellationToken);
                await conn.ExecuteAsync(sql, parameter);
            }

            return IdentityResult.Success;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var sql = @"
                    select u.Id,
                           u.UniqueId,
                           u.UserName,
                           u.NormalizedUserName,
                           u.FirstName,
                           u.LastName,
                           u.Email,
                           u.NormalizedEmail,
                           u.PasswordHash,
                           u.Created,
                           u.Updated,
                           r.Id,
                           r.UniqueId,
                           r.Name,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleUniqueId = r.UniqueId
                    where u.UniqueId = @UniqueId and u.IsDeleted = 0";

            using (var conn = new SqlConnection(_connectionString))
            {
                var user = await conn.QueryAsync<User, Role, User>(sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                }, new { UniqueId = userId });

                return user.FirstOrDefault();
            }
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var sql = @"
                    select u.Id,
                           u.UniqueId,
                           u.UserName,
                           u.NormalizedUserName,
                           u.FirstName,
                           u.LastName,
                           u.Email,
                           u.NormalizedEmail,
                           u.PasswordHash,
                           u.Created,
                           u.Updated,
                           r.Id,
                           r.UniqueId,
                           r.Name,
                           r.Description,
                           r.Created,
                           r.Updated
                    from dbo.Users as u with (nolock)
                        inner join dbo.Roles as r with (nolock) on u.RoleUniqueId = r.UniqueId
                    where u.NormalizedUserName = @NormalizedUserName and u.IsDeleted = 0";

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<User, Role, User>(sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                }, new { NormalizedUserName = normalizedUserName });

                return result.FirstOrDefault();
            }
        }

        public void Dispose()
        {
            // Nothing to dispose
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sqlQuery = "select u.* from dbo.Users u where u.NormalizedEmail = @NormalizedEmail and u.IsDeleted = 0";
                return await connection.QuerySingleOrDefaultAsync<User>(sqlQuery, new { normalizedEmail = normalizedEmail });
            }
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleIdQuery = "select r.Id from dbo.Role r where r.NormalizedName = @NormalizedName";
                var roleId = await connection.ExecuteScalarAsync<int?>(roleIdQuery, new { NormalizedName = normalizedName });
                if (!roleId.HasValue)
                {
                    var insertQuery = @"insert into dbo.Roles (UniqueId, Name, NormalizedName, Description)
                                        values (NEWID(), @Name, @NormalizedName, @Description)
                                      ";
                    var parameter = new DynamicParameters();
                    parameter.Add("@Name", roleName);
                    parameter.Add("@NormalizedName", normalizedName);
                    parameter.Add("@Description", user.Role.Description ?? string.Empty);
                    roleId = await connection.ExecuteAsync(insertQuery, parameter);
                }

                var insertToUserRoleQuery = @"if not exists(select 1 from dbo.UserRole ur where ur.UserId = @UserId and ur.RoleId = @RoleId)
                                              begin
                                                insert into dbo.UserRole(UserId, RoleId)
                                                values(@UserId, @RoleId)
                                              end";
                await connection.ExecuteAsync(insertToUserRoleQuery, new { UserId = user.Id, RoleId = roleId });
            }
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sqlQuery = "select r.Id from dbo.Roles r where r.NormalizedName = @NormalizedName";
                var roleId = await connection.ExecuteScalarAsync<int?>(sqlQuery, new { NormalizedName = roleName.ToUpper() });
                if (!roleId.HasValue)
                {
                    var deleteQuery = "delete from dbo.UserRole ur where ur.UserId = @UserId and ur.RoleId = @RoleId";
                    await connection.ExecuteAsync(deleteQuery, new { UserId = user.Id, RoleId = roleId });
                }
            }
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sqlQuery = "select r.Name from dbo.Roles r inner join dbo.UserRole ur on ur.RoleId = r.Id where ur.UserId = @UserId";
                var queryResults = await connection.QueryAsync<string>(sqlQuery, new { UserId = user.Id });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQueryGetId = "select r.Id from dbo.Roles r where r.NormalizedName = @NormalizedName";
                var roleId = await connection.ExecuteScalarAsync<int?>(sqlQueryGetId, new { NormalizedName = roleName.ToUpper() });

                if (roleId == default(int))
                {
                    return false;
                }

                var sqlQueryGetMatchingRole = "select count(*) from dbo.UserRole ur where ur.UserId = @UserId and ur.RoleId = @RoleId";
                var matchingRoles = await connection.ExecuteScalarAsync<int>(sqlQueryGetMatchingRole, new { UserId = user.Id, RoleId = roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"
                    select u.* from dbo.Users u
                    inner join dbo.UserRole ur on ur.UserId = u.Id
                    inner join dbo.Roles r on ur.RoleId = r.Id where r.NormalizedName = @NormalizedName and u.IsDeleted = 0";
                var queryResults = await connection.QueryAsync<User>(sqlQuery, new { NormalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }
    }
}