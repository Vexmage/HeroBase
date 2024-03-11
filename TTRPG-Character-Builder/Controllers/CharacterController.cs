using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Models;
using System.Linq; 

namespace TTRPG_Character_Builder.Controllers
{
    public class CharacterController : Controller
    {
        // Dependency injection of the database context
        private readonly ApplicationDbContext _context;

        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Character/Create
        public IActionResult Create()
        {
            ViewBag.Races = new SelectList(_context.Races, "RaceId", "Name");
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "Name");
            return View();
        }

        // POST: Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,RaceId,ClassId,Strength,Dexterity,Intelligence,Wisdom,Constitution,Charisma,Biography")] Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            ViewBag.Races = new SelectList(_context.Races, "RaceId", "Name", character.RaceId);
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "Name", character.ClassId);
            return View(character);
        }

        // GET: Character/Index with optional search functionality
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var characters = from c in _context.Characters
                             select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                characters = characters.Where(s => s.Name.Contains(searchString)
                                       || s.Race.Contains(searchString)
                                       || s.Class.Contains(searchString));
            }

            return View("List", await characters.ToListAsync());
        }

        public async Task<IActionResult> Detail(int id)
        {
            var character = await _context.Characters
                .Include(c => c.Race)
                .Include(c => c.Class)
                .FirstOrDefaultAsync(m => m.CharacterId == id);

            if (character == null)
            {
                return NotFound();
            }
            return View(character);
        }



        // GET: Character/Edit/5
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

        // POST: Character/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Race,Class,Strength,Dexterity,Intelligence,Wisdom,Constitution,Charisma,Biography")] Character character)
        {
            if (id != character.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.ID))
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
            return View(character);
        }

        // GET: Character/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Character/Delete/5
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
            return _context.Characters.Any(e => e.ID == id);
        }
    }
}
