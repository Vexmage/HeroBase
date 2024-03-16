using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
