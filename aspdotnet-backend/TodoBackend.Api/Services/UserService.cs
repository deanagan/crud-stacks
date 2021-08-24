using System.Collections.Generic;
using System.Linq;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Interfaces;
using System.Text;

using System.Threading.Tasks;

namespace TodoBackend.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserView>> GetUsersAsync()
        {
            var users = from user in await _unitOfWork.Users.GetAllAsync()
                        join role in await _unitOfWork.Roles.GetAllAsync()
                        on user.RoleId equals role.Id
                        select new UserView
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Hash = user.Hash,
                            Role = role
                        };

            return users;
        }

        public IEnumerable<UserView> GetUsers()
        {
            var users = from user in _unitOfWork.Users.GetAll()
                        join role in _unitOfWork.Roles.GetAll()
                        on user.RoleId equals role.Id
                        select new UserView
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Hash = user.Hash,
                            Role = role
                        };

            return users;
        }

        public async Task<UserView> GetUser(int id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);

            return user != null ? new UserView
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Hash = user.Hash,
                Role = await _unitOfWork.Roles.GetAsync(user.RoleId)
            } : null;
        }

        public async void CreateUser(User user)
        {
            var newUser = new User
            {
                Name = user.Name,
                RoleId = user.RoleId,
                Email = user.Email,
                Hash = "todo123"
            };

            await _unitOfWork.Users.AddAsync(newUser);

            _unitOfWork.Save();
            user.Id = newUser.Id;
            user.Role = await _unitOfWork.Roles.GetAsync(user.RoleId);
        }

        public bool UpdateUser(User user)
        {
            if (user != null)
            {
                _unitOfWork.Users.Update(user);
                _unitOfWork.Save();
            }

            return user != null;
        }

        public bool DeleteUser(int id)
        {
            var user = _unitOfWork.Users.Get(id);
            if (user != null)
            {
                _unitOfWork.Users.Delete(user);
                _unitOfWork.Save();
            }

            return user != null;
        }
    }
}
