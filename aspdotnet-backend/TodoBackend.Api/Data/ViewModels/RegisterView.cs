using System;
using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public Guid RoleUniqueId { get; set; }
    }
}