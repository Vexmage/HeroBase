using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class CommentViewModel
    {
        [Required]
        public int CharacterId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The comment must be between 3 and 500 characters long.", MinimumLength = 3)]
        public string Content { get; set; }
    }
}
