using Core.Models;
using Infrastructure.Data;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly DatabaseContext _context;
        public GenericRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T Base)
        {
            _context.Set<T>().Add(Base);
            await _context.SaveChangesAsync();
            return Base;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is null)
            {
                return entity!;
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await _context.Set<T>().FindAsync(id);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> UpdateAsync(T Base)
        {
            if(await TExists(Base.Id))
            {
                _context.Entry(Base).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Base;
            }

            return null!;
        }

        private async Task<bool> TExists(int id)
        {
            return await _context.Set<T>().AnyAsync(c => c.Id == id);
        }
    }
}