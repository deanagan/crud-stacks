using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Data.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}
