using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace QomekAppAuth
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
