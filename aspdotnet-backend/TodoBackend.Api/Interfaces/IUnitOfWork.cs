using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Interfaces
{
    public interface IUnitOfWork
    {
        IDataRepository<User> Users { get; }
        IDataRepository<Role> Roles { get; }
        void Save();
    }
}
