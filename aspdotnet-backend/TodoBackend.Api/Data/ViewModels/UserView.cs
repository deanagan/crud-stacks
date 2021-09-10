using System;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Data.ViewModels
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
        public DateTime Updated { get; set; }
        public Role Role { get; set; }
    }
}
