using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TTRPG_Character_Builder.Controllers;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Models;
using Xunit;

namespace TTRPGtests.Controllers
{
    public class CharacterControllerTests
    {
        // Test Details action method
        [Fact]
        public async Task Details_ReturnsViewResult_WithValidCharacter()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();
            var characterId = 1;
            var character = new Character { CharacterId = characterId, Name = "Test Character" };
            mockContext.Setup(m => m.Characters.FindAsync(characterId)).ReturnsAsync(character);
            var controller = new CharacterController(mockContext.Object, null);

            // Act
            var result = await controller.Details(characterId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Character>(viewResult.ViewData.Model);
            Assert.Equal(character, model);
        }

        // Test Create action method
        [Fact]
        public async Task Create_ReturnsViewResult_WithCharacterModel()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();
            var controller = new CharacterController(mockContext.Object, null);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Character>(viewResult.ViewData.Model);
        }

        // Test Edit action method
        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();
            var controller = new CharacterController(mockContext.Object, null);

            // Act
            var result = await controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Delete action method
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();
            var controller = new CharacterController(mockContext.Object, null);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Add more tests for other scenarios and action methods...
    }
}
