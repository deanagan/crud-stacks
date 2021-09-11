using System;
using System.ComponentModel.DataAnnotations;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Data.ViewModels
{
    public class UserView
    {
        public Guid UniqueId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Hash { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public RoleView Role { get; set; }
    }
}
