using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set;}

        // [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

//        [Required]
        public string Hash { get; set; }

    }
}
