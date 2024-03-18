using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class PartyCharacter
    {
        [Key]
        public int Id { get; set; }

        // Foreign key for Character
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        // Foreign key for Party
        public int PartyId { get; set; }
        public Party Party { get; set; }
    }
}
