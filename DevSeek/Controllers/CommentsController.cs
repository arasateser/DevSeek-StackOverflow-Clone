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
        // In-memory data storage
        private static List<Comment> _comments = new List<Comment>();
        private static List<User> _users = new List<User>(); // Assuming this is the same list used in UsersController
        private static List<Question> _questions = new List<Question>(); // Assuming this is the same list used in QuestionsController

        // GET: /Comments/Create
        public IActionResult Create(int questionId)
        {
            // Pass the question ID to the view
            ViewBag.QuestionId = questionId;

            // Return the view for creating a new comment
            return View();
        }

        // POST: /Comments/Create
        [HttpPost]
        public IActionResult Create(int questionId, Comment comment)
        {
            // Find the current logged-in user
            var userName = HttpContext.Session.GetString("UserName");
            var user = _users.FirstOrDefault(u => u.UserName == userName);

            // Find the question to comment on
            var question = _questions.FirstOrDefault(q => q.Id == questionId);

            if (user != null && question != null)
            {
                // Set the author and question for the comment, then add it to the user's, question's, and global lists
                comment.Author = user;
                comment.Question = question;
                user.Comments.Add(comment);
                question.Comments.Add(comment);
                _comments.Add(comment);
            }

            // Redirect to the question details page
            return RedirectToAction("Details", "Questions", new { id = questionId });

        }
    }
}