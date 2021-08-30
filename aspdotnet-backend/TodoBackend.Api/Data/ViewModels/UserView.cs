using System;
using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Data.Models
{
    public class UserView
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Hash { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public Role Role { get; set; }
    }
}
