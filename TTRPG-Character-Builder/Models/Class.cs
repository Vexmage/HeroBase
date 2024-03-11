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
        
    }
}