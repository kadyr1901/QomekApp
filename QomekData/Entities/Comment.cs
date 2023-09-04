using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QomekData.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int BlogId { get; set; }
        public virtual Blog? Blog { get; set; }
        public string Text { get; set; }
    }
}
