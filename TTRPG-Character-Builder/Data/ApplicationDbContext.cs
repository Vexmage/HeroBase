using Microsoft.EntityFrameworkCore;
using TTRPG_Character_Builder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace TTRPG_Character_Builder.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Keep your DbSet properties for other entities
        public DbSet<Character> Characters { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Class> Classes { get; set; }
        // No need for DbSet<User>, since Identity handles users
    }
}