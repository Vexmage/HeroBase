using System;
using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } // Ensure this is hashed in the actual implementation

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    }
}
