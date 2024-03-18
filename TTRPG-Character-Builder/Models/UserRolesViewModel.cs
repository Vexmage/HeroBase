using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TTRPG_Character_Builder.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();
        public IList<string> UserRoles { get; set; }
    }
}
