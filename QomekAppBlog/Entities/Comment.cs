namespace QomekAppBlog.Entities
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
