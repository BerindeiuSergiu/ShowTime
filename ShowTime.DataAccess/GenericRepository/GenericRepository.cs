using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ShowTime.DataAccess.GenericInterface;

namespace ShowTime.DataAccess.GenericRepository
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        //fiecare operatia care modifica intr-un fel baza de date e urmata de
        // _context.SaveChangesAsync() pentru a salva modificarile in baza de date


        //alt lucru important _context.Set<T> => operatii pe entitatea specificata, gen setam o anumita entitate
        //pe contextul nostru
        public GenericRepository(DbContext context) 
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /*
         * posibils sa fie nevoie de un context specific, de exemplu ShowTimeContext
        public GenericRepository(ShowTimeContext context) 
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        } 
         */

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while creating the entity.", e);

            }
        }
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch(Exception e)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the entity.", e);
            }
        }
        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while updating the entity.", e);
            }
        }
        public void Delete(T entity)
        {
            try
            { 
                _context.Set<T>().Remove(entity); // remove entity from the context
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while deleting the entity.", e);
            }
        }
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id); // cautam dupa id
                if (entity == null)
                {
                    //exceptie in cazul in care a esuat extragerea
                    throw new KeyNotFoundException($"Entity with ID {id} not found.");
                }
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving the entity by ID.", e);
            }
        }
        async Task<IEnumerable<T>> IGenericRepository<T>.GetAllAsync()
        {
            try
            {
                // returneaza toate entitatile din baza de date
                //sub forma de lista bineinteles
               return await _context.Set<T>().ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving all entities.", e);
            }
        }
    }
}
