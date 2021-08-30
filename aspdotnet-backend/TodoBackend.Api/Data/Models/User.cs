using System;
using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Hash { get; set; }
        public DateTime Created { get; set; }
        public int RoleId { get; set; }
    }
}
