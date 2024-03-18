using System.ComponentModel.DataAnnotations;
using TTRPG_Character_Builder.Models;


namespace TTRPG_Character_Builder.Models
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Biography { get; set; }

        // Navigation properties for race and class
        public int RaceId { get; set; }
        public Race Race { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int PartyId { get; set; }
        public Party Party { get; set; }

        // Method to update character attributes based on race and class
        public void UpdateAttributes()
        {
            if (Race != null && Class != null)
            {
                // Update attributes based on race and class bonuses
                Strength = Race.StrBonus + Class.StrBonus;
                Dexterity = Race.DexBonus + Class.DexBonus;
                Constitution = Race.ConBonus + Class.ConBonus;
                Intelligence = Race.IntBonus + Class.IntBonus;
                Wisdom = Race.WisBonus + Class.WisBonus;
                Charisma = Race.ChaBonus + Class.ChaBonus;
            }
        }

        // Attributes updated based on race and class
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
    }
}
