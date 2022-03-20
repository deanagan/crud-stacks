using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Data.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}
