using IdentityDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityDAL
{
    public class IdentityDbContext: DbContext
    {
        public IdentityDbContext()
        {
        }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.UserId);
            modelBuilder.Entity<Role>()
                .HasKey(e => e.RoleId);
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
