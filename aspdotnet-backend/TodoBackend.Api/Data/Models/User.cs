using System;

namespace TodoBackend.Api.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public Role Role { get; set;}
    }
}
