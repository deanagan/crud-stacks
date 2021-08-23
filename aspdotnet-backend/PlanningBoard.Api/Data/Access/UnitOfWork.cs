using PlanningBoard.Api.Data.Models;
using PlanningBoard.Api.Data.Access;
using PlanningBoard.Api.Interfaces;

namespace PlanningBoard.Api.Data.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private DataRepository<User> _users;
        private DataRepository<Role> _roles;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IDataRepository<User> Users
        {
            get { return _users ?? (_users = new DataRepository<User>(_context)); }
        }

        public IDataRepository<Role> Roles
        {
            get { return _roles ?? (_roles = new DataRepository<Role>(_context));}
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
