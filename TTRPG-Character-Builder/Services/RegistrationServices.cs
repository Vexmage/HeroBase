using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TTRPG_Character_Builder.Models;
using TTRPG_Character_Builder.ViewModels;

namespace TTRPG_Character_Builder.Services
{
    public class RegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistrationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
    }
}
