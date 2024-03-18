using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // I'll add other properties as needed for editing users
    }
}
