using Xunit;
using Moq;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Controllers;
using TTRPG_Character_Builder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TTRPGtests.Controllers
{
    public class LoginTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly UserController _userController;

        public LoginTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            // Create a test user with known credentials
            var testUser = new User
            {
                Username = "TestUser",
                Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("TestPassword")),
                Email = "test@example.com"
            };
            _context.Users.Add(testUser);
            _context.SaveChanges();

            _userController = new UserController(_context);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldRedirectToIndex()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Username = "TestUser",
                Password = "TestPassword"
            };

            // Act
            var result = await _userController.Login(loginViewModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }


}
