using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace TTRPG_Character_Builder.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql("Server=localhost;Database=ttrpgvault;Uid=root;Pwd=87Wodahs87!;",
                                    ServerVersion.AutoDetect("Server=localhost;Database=ttrpgvault;Uid=root;Pwd=87Wodahs87!;"));


            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

}
