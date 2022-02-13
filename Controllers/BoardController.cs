using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebApplication.Data;
using ToDoWebApplication.Models;

namespace ToDoWebApplication.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Boards.ToListAsync());
        }
        public async Task<IActionResult> DisplayAll(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View(await _context.ToDos.Where(a => a.BoardId == id).Include(a => a.Board).ToListAsync());
        }
        public async Task<IActionResult> DisplayPending(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View(await _context.ToDos.Where(a => a.BoardId == id && a.Status == false).Include(a => a.Board).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Board board)
        {
            if (ModelState.IsValid)
            {
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Board board)
        {
            if (id != board.Id)
            {
                return NotFound();
            }
            if (await _context.Boards.AnyAsync(a => a.Name == board.Name && a.Id != id))
            {
                ModelState.AddModelError("Name", $"{board.Name} already exist");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(board);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.Id))
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
            return View(board);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(int id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> NameExists(string name, int id)
        {
            if (id > 0)
            {
                return Json(true);
            }
            else
            {
                return Json(!await _context.Boards.AnyAsync(c => c.Name == name));
            }

        }
    }
}
