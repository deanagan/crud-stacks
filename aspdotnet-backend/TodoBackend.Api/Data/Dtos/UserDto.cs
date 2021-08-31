using System;

namespace TodoBackend.Api.Data.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int RoleId { get; set; }
        public Guid RoleUniqueId { get; set; }
        public string RoleKind { get; set; }
        public string RoleDescription { get; set; }
        public DateTime RoleCreated { get; set; }
        public DateTime RoleUpdated { get; set; }
    }
}
