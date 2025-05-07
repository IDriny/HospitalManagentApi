using HospitalManagentApi.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class GenericRepo <T>:IGenericRepo<T> where T : class
    {
        private readonly Persistence.HospitalDbContext _context;

        public GenericRepo(Persistence.HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }
    }
}
