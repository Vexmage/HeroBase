using System.ComponentModel.DataAnnotations;
using TTRPG_Character_Builder.Models;
using TTRPG_Character_Builder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace TTRPGtests.Models
{
    public class CharacterTests
    {
        [Fact]
        public void CharacterModel_Validation_ShouldPassWithValidData()
        {
            var character = new Character
            {
                Name = "Legolas",
                Race = new Race { RaceId = 1, Name = "Elf", StrBonus = 2, DexBonus = 2 },
                Class = new Class { ClassId = 1, Name = "Ranger", Description = "A skilled ranger." },
                Strength = 17,
                Dexterity = 17,
                Constitution = 14,
                Intelligence = 15,
                Wisdom = 12,
                Charisma = 16,
                Biography = "This is a test character."
            };

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(character, new ValidationContext(character), validationResults, true);

            Assert.True(actual, "Expected validation to succeed with valid data.");
        }


        [Fact]
        public void CharacterModel_Validation_ShouldFailWithInvalidData()
        {
            var character = new Character
            {
                Strength = 10,
                Dexterity = 10,
                Intelligence = 10,
                Wisdom = 10,
                Constitution = 10,
                Charisma = 10,
                Biography = "Biography of a character without a name, race, or class."
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(character, null, null);
            var actual = Validator.TryValidateObject(character, validationContext, validationResults, true);

            Assert.False(actual, "Expected validation to fail with invalid data.");
        }

    }
}
