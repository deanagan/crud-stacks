using System.Collections.Generic;
using System.Threading.Tasks;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<Task>> GetTasksAsync();
        IEnumerable<UserView> GetUsers();
        Task<UserView> GetUser(int id);
        void CreateUser(User user);
        bool UpdateUser(User user);

        bool DeleteUser(int id);
    }

}
