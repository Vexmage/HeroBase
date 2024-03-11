using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class ApplicationUser : IdentityUser
    {
        // The ID property is inherited from IdentityUser

        // // Username is managed as UserName in IdentityUser
        // 
        // // Password is managed internally by IdentityUser, so no need to add it here
        // 
        // // Email is also managed by IdentityUser

        // Custom property from User class
        [DataType(DataType.DateTime)]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        
    }
}