using Xunit;
using Moq;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Controllers;
using TTRPG_Character_Builder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TTRPGtests.Controllers
{
    public class RegisterTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly UserController _userController;

        public RegisterTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _userController = new UserController(_context);
        }

        [Fact]
        public async Task Register_WithValidData_ShouldRedirectToLogin()
        {
            // Arrange
            var viewModel = new RegisterViewModel
            {
                Username = "NewUser",
                Email = "newuser@example.com",
                Password = "password" 
            };

            // Act
            var result = await _userController.Register(viewModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectResult.ActionName);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

}
