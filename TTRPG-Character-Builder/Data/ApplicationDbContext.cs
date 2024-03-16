using Microsoft.EntityFrameworkCore;
using TTRPG_Character_Builder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.IO;

namespace TTRPG_Character_Builder.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Party> Parties { get; set; } // Add this line for Party DbSet

        // If you decide that characters can belong to parties, you might need to configure the relationship here.
        // For example, if you have a navigation property in Characters for Parties, or vice versa.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure this is called first to configure ASP.NET Core Identity

            // Example of configuring a one-to-many relationship between Party and Characters
            // If you have a navigation property in Party for Characters:
            modelBuilder.Entity<Party>()
                .HasMany(p => p.Characters) // Assuming Party has a collection of Characters
                .WithOne(c => c.Party) // Assuming Character has a Party navigation property
                .HasForeignKey(c => c.PartyId); // Assuming Character has a PartyId foreign key

            // Other model configurations...
        }
    }
}
