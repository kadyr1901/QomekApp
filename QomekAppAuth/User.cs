namespace QomekAppAuth
{
    using Microsoft.AspNetCore.Identity;
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
