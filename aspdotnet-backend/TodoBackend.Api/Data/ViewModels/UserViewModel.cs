using System;
using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Data.ViewModels
{
    public class UserViewModel
    {
        public Guid UniqueId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
