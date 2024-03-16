using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

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
            ViewBag.Races = new SelectList(_context.Races, "RaceId", "Name");
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "Name");
            ViewBag.Parties = new SelectList(_context.Parties, "PartyId", "Name");
            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,Name,Level,Health,Mana,RaceId,ClassId,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma,BaseAttackBonus,ArmorClassBonus,HitPoints,Biography,ApplicationUserId,PartyId")] Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Races = new SelectList(_context.Races, "RaceId", "Name", character.RaceId);
            ViewBag.Classes = new SelectList(_context.Classes, "ClassId", "Name", character.ClassId);
            ViewBag.Parties = new SelectList(_context.Parties, "PartyId", "Name", character.PartyId);
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
            ViewBag.Parties = new SelectList(_context.Parties, "PartyId", "Name", character.PartyId);
            return View(character);
        }

        // POST: Characters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterId,Name,Level,Health,Mana,RaceId,ClassId,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma,BaseAttackBonus,ArmorClassBonus,HitPoints,Biography,ApplicationUserId,PartyId")] Character character)
        {
            if (id != character.CharacterId)
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
            ViewBag.Parties = new SelectList(_context.Parties, "PartyId", "Name", character.PartyId);
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
