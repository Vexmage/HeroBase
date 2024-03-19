using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
