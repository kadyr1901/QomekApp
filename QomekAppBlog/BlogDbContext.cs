using Microsoft.EntityFrameworkCore;
using QomekAppBlog.Entities;

namespace QomekAppBlog
{
    public class BlogDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
