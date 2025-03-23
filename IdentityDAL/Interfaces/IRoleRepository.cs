using IdentityDAL.Entities;

namespace IdentityDAL.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(Guid id);
        Task AddAsync(Role entity);
        Task UpdateAsync(Role entity);
        Task DeleteAsync(Guid id);
    }
}
