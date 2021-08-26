using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Data.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Hash { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }
    }
}
