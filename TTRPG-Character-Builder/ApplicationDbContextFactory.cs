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
            optionsBuilder.UseMySql("Server=mysql8003.site4now.net;Database=db_aa21a3_herobas;Uid=aa21a3_herobas;Pwd=Snarpian87!;",
                                    ServerVersion.AutoDetect("Server=mysql8003.site4now.net;Database=db_aa21a3_herobas;Uid=aa21a3_herobas;Pwd=Snarpian87!;"));


            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

}
