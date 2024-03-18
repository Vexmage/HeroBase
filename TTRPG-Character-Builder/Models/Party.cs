using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class Party
    {
        [Key]
        public int PartyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Define the many-to-many relationship with characters
        public ICollection<PartyCharacter> Characters { get; set; }
    }
}
