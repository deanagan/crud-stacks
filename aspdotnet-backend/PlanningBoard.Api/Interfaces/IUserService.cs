using System.Collections.Generic;
using System.Threading.Tasks;
using PlanningBoard.Api.Data.Models;

namespace PlanningBoard.Api.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserView>> GetUsersAsync();
        IEnumerable<UserView> GetUsers();
        Task<UserView> GetUser(int id);
        void CreateUser(User user);
        bool UpdateUser(User user);

        bool DeleteUser(int id);
    }

}
