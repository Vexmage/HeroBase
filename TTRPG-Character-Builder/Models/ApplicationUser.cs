using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace TTRPG_Character_Builder.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Custom property for the date the user joined
        [DataType(DataType.DateTime)]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    }
}
