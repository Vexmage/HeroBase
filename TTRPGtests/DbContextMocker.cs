using Microsoft.EntityFrameworkCore;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Models;
using Microsoft.AspNetCore.Mvc;


public static class DbContextMocker
{
    public static ApplicationDbContext GetApplicationDbContext(string dbName)
    {
        // Create options for DbContext instance
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        // Create instance of DbContext
        var dbContext = new ApplicationDbContext(options);

        // Add entities in memory
        dbContext.Seed();

        return dbContext;
    }

    private static void Seed(this ApplicationDbContext context)
    {
        // Your seeding logic here
        // For example:
        context.Characters.AddRange(
            new Character { /* ... properties ... */ },
            new Character { /* ... properties ... */ }
            // etc.
        );

        context.SaveChanges();
    }
}