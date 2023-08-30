namespace QomekAppBlog.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string Title { get; set; }
        public string Body { get; set;}
        public virtual List<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
