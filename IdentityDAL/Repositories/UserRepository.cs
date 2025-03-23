using IdentityDAL.Entities;
using IdentityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityDAL.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly IdentityDbContext _context;

        public UserRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(p => p.UserId == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(p => p.UserId == id);
            if (entity != null)
            {
                _context.Users.Remove(entity);
            }
        }
    }
}
