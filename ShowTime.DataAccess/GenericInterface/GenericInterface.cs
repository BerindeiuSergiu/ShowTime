using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.GenericInterface
{
    public interface IGenericRepository<T> where T : class
    {
        // T -> generic class
        /*
         * 
         * Usage method : public class I..: IGenericRepository<Artist>
         * =====> se poate apela operatia CRUD pe clasa artist odata ce interfata este utilizata
         * 
         */

        Task<T> CreateAsync(T entity);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
