using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTRPG_Character_Builder.Models
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("Race")]
        public int? RaceId { get; set; } // Make nullable if not every character is associated with a race
        public virtual Race Race { get; set; }

        [ForeignKey("Class")]
        public int? ClassId { get; set; } // Make nullable if not every character is associated with a class
        public virtual Class Class { get; set; }

        [ForeignKey("Party")]
        public int? PartyId { get; set; } // Make nullable if not every character is in a party
        public virtual Party Party { get; set; }

        public string ApplicationUserId { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int Mana { get; set; }

        [StringLength(1000)]
        public string Biography { get; set; }
    }
}

/*
// Method to update character attributes based on race and class
public void UpdateAttributes()
    {
        if (Race != null && Class != null)
        {
            Strength += Race.StrBonus + Class.StrBonus;
            Dexterity += Race.DexBonus + Class.DexBonus;
            Constitution += Race.ConBonus + Class.ConBonus;
            Intelligence += Race.IntBonus + Class.IntBonus;
            Wisdom += Race.WisBonus + Class.WisBonus;
            Charisma += Race.ChaBonus + Class.ChaBonus;
        }
    }
}
}
*/