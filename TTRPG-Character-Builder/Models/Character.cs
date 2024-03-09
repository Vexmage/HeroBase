using System;
using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class Character
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string Race { get; set; }

        [Required]
        public string Class { get; set; }
        public int BaseAttackBonus { get; set; }
        public int ArmorClassBonus { get; set; }
        public int HitPoints { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Constitution { get; set; }
        public int Charisma { get; set; }

        [StringLength(1000)]
        public string? Biography { get; set; }

        // ForeignKey for User
        public int UserID { get; set; }
    }
}
