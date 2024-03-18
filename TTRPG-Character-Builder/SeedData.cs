using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTRPG_Character_Builder.Models;

namespace TTRPG_Character_Builder.Data
{
    public static class SeedData
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            await SeedRolesAsync(roleManager);
            await SeedAdminAsync(userManager);
            await SeedRacesAndClassesAsync(context);
            await SeedCharactersAsync(context);
            await SeedPartiesAsync(context);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Guest", "RegisteredUser", "PartyLeader", "ContentCreator", "Administrator", "Moderator" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new ApplicationUser { UserName = "admin", Email = "admin@example.com" };
                var result = await userManager.CreateAsync(user, "Admin123!Admin"); // Use a stronger password in production
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }

        private static async Task SeedRacesAndClassesAsync(ApplicationDbContext context)
        {
            if (!context.Races.Any())
            {
                var races = new List<Race>
                {
                    new Race { Name = "Human", Description = "Versatile and ambitious, humans are the most common race in many worlds." },
                    new Race { Name = "Dwarf", Description = "Dwarves are solid and enduring like the mountains they love, weathering any challenge." },
                    new Race { Name = "Elf", Description = "Elves are a magical people of otherworldly grace, living in the world but not entirely part of it." },
                    new Race { Name = "Halfling", Description = "Halflings are clever, capable, and resourceful, and they survive in a world full of larger creatures by avoiding notice or, barring that, avoiding offense." }
                };

                context.Races.AddRange(races);
                await context.SaveChangesAsync();
            }

            if (!context.Classes.Any())
            {
                var classes = new List<Class>
                {
                    new Class { Name = "Fighter", Description = "Fighters learn the basics of all combat styles. Every fighter can swing an axe, fence with a rapier, wield a longsword or a greatsword, use a bow, and even trap foes in a net with some degree of skill." },
                    new Class { Name = "Rogue", Description = "Rogues rely on skill, stealth, and their foes' vulnerabilities to get the upper hand in any situation. They have a knack for finding the solution to just about any problem, demonstrating a resourcefulness and versatility that is the cornerstone of any successful adventuring party." },
                    new Class { Name = "Wizard", Description = "Wizards are supreme magic-users, defined and united as a class by the spells they cast. Drawing on the subtle weave of magic that permeates the cosmos, wizards cast spells of explosive fire, arcing lightning, subtle deception, and brute-force mind control." },
                    new Class { Name = "Cleric", Description = "Clerics are intermediaries between the mortal world and the distant planes of the gods. As varied as the gods they serve, clerics strive to embody the handiwork of their deities." }
                };

                context.Classes.AddRange(classes);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedPartiesAsync(ApplicationDbContext context)
        {
            if (!context.Parties.Any())
            {
                // Retrieve some characters from the database to associate with parties
                var characters = await context.Characters.ToListAsync();

                // Create sample parties
                var parties = new List<Party>
        {
            new Party { Name = "Adventurers Guild", Description = "A group of brave adventurers seeking glory and treasure." }
            
        };

                // Assign characters to parties (assuming at least one character exists)
                foreach (var party in parties)
                {
                    // Instead of assigning characters directly, you can add them to the party's Characters collection
                    foreach (var character in characters)
                    {
                        party.Characters.Add(new PartyCharacter { Character = character });
                    }
                }

                // Add parties to the database
                context.Parties.AddRange(parties);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCharactersAsync(ApplicationDbContext context)
        {
            if (!context.Characters.Any())
            {
                // Create sample characters
                var characters = new List<Character>
        {
            new Character { Name = "Sample Character 1", Biography = "Biography for Sample Character 1" },
            new Character { Name = "Sample Character 2", Biography = "Biography for Sample Character 2" },
            // Add more sample characters as needed
        };

                // Add characters to the database
                context.Characters.AddRange(characters);
                await context.SaveChangesAsync();
            }
        }





    }
}
