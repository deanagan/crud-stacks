using System;

namespace TodoBackend.Api.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public Guid UserUniqueId { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string Email { get; set; }
        public DateTime Create { get; set; }
        public Role Role { get; set;}
    }
}
