using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTRPG_Character_Builder.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Content { get; set; }

        // This will be the timestamp of when the comment was posted
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        // This connects the comment to an ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        // ForeignKey for Character
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
