using Microsoft.EntityFrameworkCore;
using QomekData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QomekData
{
    public class QomekDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public QomekDbContext(DbContextOptions<QomekDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
