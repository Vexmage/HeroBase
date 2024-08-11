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
    public class RegisterTests : IDisposable
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly UserController _userController;

        public RegisterTests()
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

            // Mock a successful user creation and sign-in
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockSignInManager.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null))
                .Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task Register_WithValidData_ShouldRedirectToHomeIndex()
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
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Home", redirectResult.ControllerName);
        }

        public void Dispose()
        {
            // Dispose resources if needed
        }
    }
}
