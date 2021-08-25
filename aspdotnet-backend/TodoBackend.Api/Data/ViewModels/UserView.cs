

namespace TodoBackend.Api.Data.Models
{
    public class UserView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Role Role { get; set;}

        public string Email { get; set; }

        public string Hash { get; set; }
    }
}
