using Core.Models;

namespace Infrastructure.Repository.Interfaces
{
    public interface IGenericRepository<T> where T: BaseModel
    {
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T Base);
        Task<T> UpdateAsync(T Base);
        Task<T> DeleteAsync(int id);
    }
}