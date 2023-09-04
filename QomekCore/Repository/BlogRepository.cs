using Microsoft.EntityFrameworkCore;
using QomekData;
using QomekData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QomekCore.Repository
{
    public class BlogRepository : QomekRepository<Blog>
    {
        public BlogRepository(QomekDbContext db) : base(db)
        {
        }

        public async Task AddComments(List<Comment> comments, int blogId)
        {
            var blog=await _db.Set<Blog>().FirstOrDefaultAsync(b => b.Id == blogId);
            if(blog!=null)
            {
                blog.Comments.AddRange(comments);
            }
        }
    }
}
