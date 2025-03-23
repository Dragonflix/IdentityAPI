using IdentityDAL.Entities;
using IdentityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityDAL.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly IdentityDbContext _context;

        public RoleRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _context.Roles.FirstOrDefaultAsync(p => p.RoleId == id);
        }

        public async Task AddAsync(Role entity)
        {
            await _context.Roles.AddAsync(entity);
        }

        public async Task UpdateAsync(Role entity)
        {
            _context.Roles.Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Roles.FirstOrDefaultAsync(p => p.RoleId == id);
            if (entity != null)
            {
                _context.Roles.Remove(entity);
            }
        }
    }
}
