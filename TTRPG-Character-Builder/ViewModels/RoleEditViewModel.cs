using System.Collections.Generic;
using TTRPG_Character_Builder.Models; 


namespace TTRPG_Character_Builder.ViewModels
{
    public class RoleEditViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public List<ApplicationUser> NonMembers { get; set; }

        public RoleEditViewModel()
        {
            Members = new List<ApplicationUser>();
            NonMembers = new List<ApplicationUser>();
        }
    }
}
