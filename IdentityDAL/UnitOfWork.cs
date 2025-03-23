using IdentityDAL.Interfaces;
using IdentityDAL.Repositories;

namespace IdentityDAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _context;

        public IUserRepository UserRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public UnitOfWork(IdentityDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
