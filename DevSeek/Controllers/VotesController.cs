using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using DevSeek.Data;
using DevSeek.Models;

namespace DevSeek.Controllers
{
    [Authorize]
    public class VotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Votes/Upvote
        // Handles upvoting a question
        [HttpPost]
        public async Task<IActionResult> Upvote(int questionId)
        {
            var userId = User.Identity.Name;
            var existingVote = _context.Votes.FirstOrDefault(v => v.QuestionId == questionId && v.UserId == userId);

            if (existingVote == null)
            {
                var vote = new Vote
                {
                    IsUpvote = true,
                    UserId = userId,
                    QuestionId = questionId
                };
                _context.Add(vote);
            }
            else if (!existingVote.IsUpvote)
            {
                existingVote.IsUpvote = true;
                _context.Update(existingVote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Questions", new { id = questionId });
        }

        // POST: Votes/Downvote
        // Handles downvoting a question
        [HttpPost]
        public async Task<IActionResult> Downvote(int questionId)
        {
            var userId = User.Identity.Name;
            var existingVote = _context.Votes.FirstOrDefault(v => v.QuestionId == questionId && v.UserId == userId);

            if (existingVote == null)
            {
                var vote = new Vote
                {
                    IsUpvote = false,
                    UserId = userId,
                    QuestionId = questionId
                };
                _context.Add(vote);
            }
            else if (existingVote.IsUpvote)
            {
                existingVote.IsUpvote = false;
                _context.Update(existingVote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Questions", new { id = questionId });
        }
    }
}