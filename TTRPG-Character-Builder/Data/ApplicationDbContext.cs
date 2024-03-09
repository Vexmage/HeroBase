using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TTRPG_Character_Builder.Models;

namespace TTRPG_Character_Builder.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
