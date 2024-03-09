using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Models;

var builder = WebApplication.CreateBuilder(args);

// Add configuration settings
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add your database context service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed the database with initial data
SeedDatabase(app);

app.Run();

void SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated(); // Ensure the database is created

            // Clear existing data
            Console.WriteLine("Clearing existing data in Characters table...");
            context.Characters.RemoveRange(context.Characters);
            context.SaveChanges();
            Console.WriteLine("Existing data cleared.");

            // Seed new data
            Console.WriteLine("Seeding new data...");
            context.Characters.AddRange(
new Character
{
    Name = "Aelar",
    Race = "Elf",
    Class = "Ranger",
    Strength = 14,
    Dexterity = 15,
    Intelligence = 12,
    Wisdom = 13,
    Constitution = 10,
    Charisma = 12,
    Biography = "A ranger from the misty forests."
},
                    new Character
                    {
                        Name = "Eldon",
                        Race = "Halfling",
                        Class = "Thief",
                        Strength = 8,
                        Dexterity = 17,
                        Intelligence = 13,
                        Wisdom = 10,
                        Constitution = 12,
                        Charisma = 14,
                        Biography = "A light-footed halfling with a penchant for finding trouble."
                    },
                    new Character
                    {
                        Name = "Mirabelle",
                        Race = "Human",
                        Class = "Paladin",
                        Strength = 16,
                        Dexterity = 10,
                        Intelligence = 12,
                        Wisdom = 14,
                        Constitution = 15,
                        Charisma = 13,
                        Biography = "A devoted paladin serving the Order of the Light."
                    },
                    new Character
                    {
                        Name = "Thokk",
                        Race = "Orc",
                        Class = "Barbarian",
                        Strength = 18,
                        Dexterity = 12,
                        Intelligence = 8,
                        Wisdom = 10,
                        Constitution = 16,
                        Charisma = 6,
                        Biography = "A fearsome warrior with unmatched battle fury."
                    },
                    new Character
                    {
                        Name = "Seraphina",
                        Race = "Elf",
                        Class = "Wizard",
                        Strength = 8,
                        Dexterity = 14,
                        Intelligence = 17,
                        Wisdom = 12,
                        Constitution = 10,
                        Charisma = 11,
                        Biography = "An elven wizard who seeks the arcane knowledge of the ages."
                    },
                    new Character
                    {
                        Name = "Brom",
                        Race = "Dwarf",
                        Class = "Cleric",
                        Strength = 14,
                        Dexterity = 8,
                        Intelligence = 12,
                        Wisdom = 17,
                        Constitution = 16,
                        Charisma = 10,
                        Biography = "A devout cleric with a heart as sturdy as the mountains."
                    },
                    new Character
                    {
                        Name = "Luna",
                        Race = "Tiefling",
                        Class = "Bard",
                        Strength = 10,
                        Dexterity = 14,
                        Intelligence = 12,
                        Wisdom = 10,
                        Constitution = 12,
                        Charisma = 18,
                        Biography = "A charismatic tiefling whose music enchants all who listen."
                    },
                    new Character
                    {
                        Name = "Garrick",
                        Race = "Human",
                        Class = "Fighter",
                        Strength = 16,
                        Dexterity = 13,
                        Intelligence = 10,
                        Wisdom = 12,
                        Constitution = 14,
                        Charisma = 10,
                        Biography = "A battle-hardened fighter seeking to avenge a fallen comrade."
                    },
                    new Character
                    {
                        Name = "Nyssa",
                        Race = "Gnome",
                        Class = "Druid",
                        Strength = 8,
                        Dexterity = 14,
                        Intelligence = 12,
                        Wisdom = 16,
                        Constitution = 13,
                        Charisma = 10,
                        Biography = "A gnome druid who speaks for the trees and the wilds."
                    },
                    new Character
                    {
                        Name = "Korinn",
                        Race = "Dragonborn",
                        Class = "Sorcerer",
                        Strength = 10,
                        Dexterity = 12,
                        Intelligence = 14,
                        Wisdom = 10,
                        Constitution = 14,
                        Charisma = 16,
                        Biography = "A dragonborn sorcerer with a mysterious draconic heritage."
                    },
                    new Character
                    {
                        Name = "Valen",
                        Race = "Half-Elf",
                        Class = "Rogue",
                        Strength = 12,
                        Dexterity = 18,
                        Intelligence = 14,
                        Wisdom = 11,
                        Constitution = 12,
                        Charisma = 14,
                        Biography = "A cunning half-elf rogue who always has an ace up his sleeve."
                    }
            );
            context.SaveChanges();
            Console.WriteLine("New data seeded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while seeding the database: " + ex.Message);
        }
    }
}
