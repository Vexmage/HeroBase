using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTRPG_Character_Builder.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
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
            // Retrieve races and classes from the database
            var races = _context.Races.ToList();
            var classes = _context.Classes.ToList();

            if (races == null || classes == null)
            {
                // Handle null collections gracefully
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Races = races;
            ViewBag.Classes = classes;

            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,Name,RaceId,ClassId,Biography")] Character character)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the selected race and class objects from the database using the provided IDs
                character.Race = await _context.Races.FindAsync(character.RaceId);
                character.Class = await _context.Classes.FindAsync(character.ClassId);

                if (character.Race == null || character.Class == null)
                {
                    // Handle null race or class gracefully
                    return RedirectToAction(nameof(Index));
                }

                // Update character attributes based on selected race and class
                character.UpdateAttributes();

                // Add the character to the context and save changes
                _context.Add(character);
                await _context.SaveChangesAsync();

                // Redirect to the index action after successful creation
                return RedirectToAction(nameof(Index));
            }
            // Handle ModelState.IsValid = false case, for example, by returning to the create view with validation errors
            return View(character);
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
