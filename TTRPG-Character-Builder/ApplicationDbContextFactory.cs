using Microsoft.AspNetCore.Hosting.Server;
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
            optionsBuilder.UseMySql("Server=MYSQL8003.site4now.net;Database=db_aa21a3_heros2;Uid=aa21a3_heros2;Pwd=TroilOcon87!;",
                                    ServerVersion.AutoDetect("Server=MYSQL8003.site4now.net;Database=db_aa21a3_heros2;Uid=aa21a3_heros2;Pwd=TroilOcon87!;"));


            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
