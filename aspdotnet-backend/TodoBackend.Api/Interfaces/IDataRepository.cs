using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoBackend.Api.Interfaces
{
    public interface IDataRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Get(params object[] parameters);
        Task<T> GetAsync(params object[] parameters);
        IQueryable<T> GetUsingRawSql(string query, params object[] parameters);
        void Add(T parameter);
        void AddRange(IEnumerable<T> parameters);
        Task AddAsync(T parameter);
        void Update(T parameter);
        void Delete(T parameter);
    }
}
