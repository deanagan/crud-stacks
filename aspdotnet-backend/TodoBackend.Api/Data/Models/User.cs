using System;

namespace TodoBackend.Api.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Role Role { get; set; }
    }
}
