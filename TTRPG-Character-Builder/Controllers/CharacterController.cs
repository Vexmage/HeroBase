using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging; 

namespace TTRPG_Character_Builder.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ApplicationDbContext context, ILogger<CharacterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Characters
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var characters = from c in _context.Characters.Include(c => c.Race).Include(c => c.Class).Include(c => c.Party)
                             select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                characters = characters.Where(s => s.Name.Contains(searchString)
                                       || s.Race.Name.Contains(searchString)
                                       || s.Class.Name.Contains(searchString)
                                       || (s.Party != null && s.Party.Name.Contains(searchString)));
            }

            return View(await characters.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.Race)
                .Include(c => c.Class)
                .Include(c => c.Party)
                .FirstOrDefaultAsync(m => m.CharacterId == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            PrepareCharacterCreationData(); // Use this method to set ViewBag data for Races and Classes dropdowns
            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,Name,RaceId,ClassId,Biography,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma")] Character character)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(character);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging purposes
                    _logger.LogError(ex, "Error occurred while saving character");

                    // Optionally, you can add an error message to the ModelState
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the character. Please try again.");
                }
            }
            // If ModelState is not valid or an exception occurred, redisplay the form with the entered data
            PrepareCharacterCreationData(); // Repopulate dropdown data if the form has issues
            return View(character);
        }


        private void PrepareCharacterCreationData()
        {
            ViewBag.Races = new SelectList(_context.Races.OrderBy(r => r.Name), "RaceId", "Name");
            ViewBag.Classes = new SelectList(_context.Classes.OrderBy(c => c.Name), "ClassId", "Name");
            // Populate ViewBag.Parties if required:
            // ViewBag.Parties = new SelectList(_context.Parties.OrderBy(p => p.Name), "PartyId", "Name");
        }





        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            ViewBag.Races = new SelectList(_context.Races, "RaceId", "Name", character.RaceId);
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "Name", character.ClassId);
            return View(character);
        }

        // POST: Characters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterId,Name,RaceId,ClassId")] Character character)
        {
            if (id != character.CharacterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the selected race and class objects from the database using the provided IDs
                    character.Race = await _context.Races.FindAsync(character.RaceId);
                    character.Class = await _context.Classes.FindAsync(character.ClassId);

                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.CharacterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Races = new SelectList(_context.Races, "RaceId", "Name", character.RaceId);
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "Name", character.ClassId);
            return View(character);
        }


        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.Race)
                .Include(c => c.Class)
                .Include(c => c.Party)
                .FirstOrDefaultAsync(m => m.CharacterId == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.CharacterId == id);
        }
    }
}
