using TTRPG_Character_Builder.Models;

public class RoleEditViewModel
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public IEnumerable<ApplicationUser> Members { get; set; }
    public IEnumerable<ApplicationUser> NonMembers { get; set; }
}
