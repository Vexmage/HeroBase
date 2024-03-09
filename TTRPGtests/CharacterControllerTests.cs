using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTRPG_Character_Builder.Models;
using TTRPG_Character_Builder.Data;
using Microsoft.AspNetCore.Mvc;
using TTRPG_Character_Builder.Controllers;
using Microsoft.EntityFrameworkCore;



namespace TTRPGtests.Controllers
{
    public class CharacterControllerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CharacterController _controller;

        public CharacterControllerTests()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            // Ensure the database is created
            _context.Database.EnsureCreated();

            // Populate the database with test data
            _context.Characters.AddRange(
                new Character { Name = "Eilodil", Race = "Elf", Class = "Warrior", Strength = 10, Dexterity = 10, Intelligence = 10, Wisdom = 10, Constitution = 10, Charisma = 10, Biography = "A brave warrior." },
                new Character { Name = "Dexter", Race = "Human", Class = "Mage", Strength = 8, Dexterity = 9, Intelligence = 15, Wisdom = 14, Constitution = 12, Charisma = 11, Biography = "A wise mage." }
            );
            _context.SaveChanges();

            // Set up the controller with the in-memory context
            _controller = new CharacterController(_context);
        }

        [Fact]
        public async Task Index_ShouldReturnViewResultWithCharacters()
        {
            // Act
            var result = await _controller.Index(null); // Pass null or an empty string for searchString

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Character>>(viewResult.Model);
            Assert.Equal(2, model.Count()); // Verify that two characters are returned
        }

        [Fact]
        public async Task Create_PostAction_ShouldAddCharacter()
        {
            // Arrange
            var newCharacter = new Character { Name = "NewCharacter", Race = "Orc", Class = "Barbarian", Strength = 12, Dexterity = 11, Intelligence = 10, Wisdom = 9, Constitution = 14, Charisma = 8, Biography = "A fierce warrior." };

            // Act
            var result = await _controller.Create(newCharacter);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(3, _context.Characters.Count()); // Assuming there were already 2 characters
        }


        [Fact]
        public async Task Edit_PostAction_ShouldUpdateCharacter()
        {
            // Arrange
            var characterToUpdate = await _context.Characters.FirstOrDefaultAsync(c => c.Name == "Eilodil");
            characterToUpdate.Biography = "Updated Biography";

            // Act
            var result = await _controller.Edit(characterToUpdate.ID, characterToUpdate);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            var updatedCharacter = await _context.Characters.FindAsync(characterToUpdate.ID);
            Assert.Equal("Updated Biography", updatedCharacter.Biography);
        }

        [Fact]
        public async Task DeleteConfirmed_PostAction_ShouldRemoveCharacter()
        {
            // Arrange
            var characterToDelete = await _context.Characters.FirstOrDefaultAsync(c => c.Name == "Dexter");

            // Act
            var result = await _controller.DeleteConfirmed(characterToDelete.ID);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.False(_context.Characters.Any(c => c.ID == characterToDelete.ID));
        }


        public void Dispose()
        {
            // Clean up the database after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
