using System.Collections.Generic;

namespace TTRPG_Character_Builder.Models
{
    public class RoleEditViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
        public List<ApplicationUser> NonMembers { get; set; } = new List<ApplicationUser>();
    }
}
