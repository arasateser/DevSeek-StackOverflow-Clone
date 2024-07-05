using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevSeek.Data;
using DevSeek.Models;

namespace DevSeek.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET Questions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.ToListAsync());
        }

        //GET Questions/Details/#
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Comments)
                //.ThenInclude(a => a.Comments)
                .Include(q => q.Comments)
                .Include(q => q.QuestionTags)
                .ThenInclude(qt => qt.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        //GET Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Questions?Create
        public async Task<IActionResult> Create([Bind("Id, Title, Body")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.CreatedAt = DateTime.Now;
                question.UserId = User.Identity.Name; //duzeltilecekkkk string-obje problemi falannnj
                _context.Add(question); //comment olmali
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        //GET Questions/Edit/#
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id, id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        //POST Questions/Edit/#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, BOdy")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExist(question.Id))
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
            return View(question);
        }

        //GET Question/Delete/#
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        //GET Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //question check if exist        
        private bool QuestionExist(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }

    }
}
