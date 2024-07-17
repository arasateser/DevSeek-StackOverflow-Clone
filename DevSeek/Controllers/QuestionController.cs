using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevSeek.Data;
using DevSeek.Models;

namespace DevSeek.Controllers
{
    public class QuestionsController : Controller
    {
        // In-memory data storage
        private static List<Question> _questions = new List<Question>();
        private static List<User> _users = new List<User>(); // Assuming this is the same list used in UsersController

        // GET: /Questions/
        public IActionResult Index()
        {
            return View(_questions);
        }

        // GET: /Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Questions/Create
        [HttpPost]
        public IActionResult Create(Question question)
        {
            // Find the current logged-in user
            var userName = HttpContext.Session.GetString("UserName");
            var user = _users.FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {
                question.Author = user;
                user.Questions.Add(question);
                _questions.Add(question);
            }

            return RedirectToAction("Index");
        }

        // GET: /Questions/Details/{id}
        public IActionResult Details(int id)
        {
            var question = _questions.FirstOrDefault(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }
    }

}
