using Xunit;
using TTRPG_Character_Builder.ViewModels; // Add this
using System.ComponentModel.DataAnnotations;

namespace TTRPGtests
{
    public class ViewModelValidationTests
    {
        [Fact]
        public void RegisterViewModel_Validation_UsernameRequired()
        {
            // Arrange
            var model = new RegisterViewModel();

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(RegisterViewModel.Username)));
        }

        [Fact]
        public void RegisterViewModel_Validation_EmailRequired()
        {
            // Arrange
            var model = new RegisterViewModel();

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(RegisterViewModel.Email)));
        }

        // Similar tests for other properties of RegisterViewModel...

        [Fact]
        public void LoginViewModel_Validation_UsernameRequired()
        {
            // Arrange
            var model = new LoginViewModel();

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(LoginViewModel.Username)));
        }

        [Fact]
        public void LoginViewModel_Validation_EmailRequired()
        {
            // Arrange
            var model = new LoginViewModel();

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(LoginViewModel.Email)));
        }

        // Similar tests for other properties of LoginViewModel...

        // Helper method to validate models
        private static System.Collections.Generic.List<ValidationResult> ValidateModel(object model)
        {
            var results = new System.Collections.Generic.List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
}
