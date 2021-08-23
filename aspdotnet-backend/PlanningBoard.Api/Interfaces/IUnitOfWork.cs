using PlanningBoard.Api.Data.Models;

namespace PlanningBoard.Api.Interfaces
{
    public interface IUnitOfWork
    {
        IDataRepository<User> Users { get; }
        IDataRepository<Role> Roles { get; }
        void Save();
    }
}
