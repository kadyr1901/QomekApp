using QomekData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QomekCore.Repository
{
    public interface IBlogRepository : IQomekRepository<Blog>
    {
        Task<Blog?> AddComments(List<Comment> comments,int blogId);
    }
}
