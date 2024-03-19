using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TTRPG_Character_Builder.Models;
using TTRPG_Character_Builder.ViewModels;

namespace TTRPG_Character_Builder.Services
{
    public class AuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
