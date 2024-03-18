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

        public DbSet<Character> Characters { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Party> Parties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Party>()
                .HasMany(p => p.Characters)
                .WithOne(c => c.Party)
                .HasForeignKey(c => c.PartyId);




            // Additional configurations for Character entity if needed
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Race)
                .WithMany()
                .HasForeignKey(c => c.RaceId);

            modelBuilder.Entity<Character>()
                .HasOne(c => c.Class)
                .WithMany()
                .HasForeignKey(c => c.ClassId);
        }
    }
}
