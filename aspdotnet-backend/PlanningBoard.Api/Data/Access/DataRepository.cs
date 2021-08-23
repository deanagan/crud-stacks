using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PlanningBoard.Api.Interfaces;
using PlanningBoard.Api.Data.Contexts;

namespace PlanningBoard.Api.Data.Access
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public DataRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetUsingRawSql(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public T Get(params object[] parameters)
        {
            return _dbSet.Find(parameters);
        }

        public async Task<T> GetAsync(params object[] parameters)
        {
            return await _dbSet.FindAsync(parameters);
        }

        public void Add(T parameter)
        {
            _dbSet.Add(parameter);
        }

        public Task AddAsync(T parameter)
        {
            _dbSet.AddAsync(parameter);
            return Task.CompletedTask;
        }

        public void AddRange(IEnumerable<T> parameters)
        {
            _dbSet.AddRange(parameters);
        }

        public void Update(T parameter)
        {
            _dbSet.Update(parameter);
        }

        public void Delete(T parameter)
        {
            _dbSet.Remove(parameter);
        }

    }
}
