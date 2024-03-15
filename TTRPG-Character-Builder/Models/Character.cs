using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTRPG_Character_Builder.Models
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }

        [ForeignKey("Race")]
        public int RaceId { get; set; }
        public Race Race { get; set; }

        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public int BaseAttackBonus { get; set; }  // Added based on UML
        public int ArmorClassBonus { get; set; }  // Added based on UML
        public int HitPoints { get; set; }        // Added based on UML

        [StringLength(1000)]
        public string? Biography { get; set; }

        // ForeignKey for ApplicationUser instead of User
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}