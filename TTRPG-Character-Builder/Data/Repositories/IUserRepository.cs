using System.Collections.Generic;
using System.Threading.Tasks;
using TTRPG_Character_Builder.Models;

namespace TTRPG_Character_Builder.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string id);
    }
}
