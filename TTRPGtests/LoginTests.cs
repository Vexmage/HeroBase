using Xunit;
using Moq;
using TTRPG_Character_Builder.Controllers;
using TTRPG_Character_Builder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TTRPGtests.Controllers
{
    public class LoginTests : IDisposable
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly UserController _userController;

        public LoginTests()
        {
            // Set up mocks for UserManager and SignInManager
            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                _mockUserManager.Object,
                contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);

            // Set up the UserController with mocked dependencies
            _userController = new UserController(_mockUserManager.Object, _mockSignInManager.Object);

            // Mock a successful sign-in
            _mockSignInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
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

        public void Dispose()
        {
            // Dispose resources if needed
        }
    }
}
