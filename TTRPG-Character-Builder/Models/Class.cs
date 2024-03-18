using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Add the missing properties
        public int StrBonus { get; set; }
        public int DexBonus { get; set; }
        public int ConBonus { get; set; }
        public int IntBonus { get; set; }
        public int WisBonus { get; set; }
        public int ChaBonus { get; set; } // Ensure this property is defined
    }
}
