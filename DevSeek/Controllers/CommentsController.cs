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
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments/Create
        public IActionResult Create(int questionId)
        {
            ViewData["QuestionId"] = questionId;
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Body,QuestionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedAt = DateTime.Now;
                comment.UserId = User.Identity.Name; //BABAYIN KEMIGI HATASI
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Questions", new { id = comment.QuestionId });
            }
            return View(comment);
        }

        //GET Comments/Edit/#
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        //POST Comments/Edit/#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Body, QuestionId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExist(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Questions", new { id = comment.QuestionId });
            }
            return View(comment);
        }

        //GET Comments/Delete/#
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }
        //POST Comments/Delete/#
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Questions", new { id = comment.QuestionId });
        }

        private bool CommentExist(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}