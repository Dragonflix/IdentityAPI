using IdentityDAL.Entities;

namespace IdentityDAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(Guid id);
        Task<User> GetByEmail(string email);
    }
}
