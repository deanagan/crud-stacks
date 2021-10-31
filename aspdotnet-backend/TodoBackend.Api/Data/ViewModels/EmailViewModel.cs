using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Data.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
