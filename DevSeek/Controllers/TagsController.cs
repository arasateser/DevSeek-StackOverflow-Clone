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
    public class TagsController : Controller
    {
        // In-memory data storage
        private static List<Tag> _tags = new List<Tag>();

        // GET: /Tags/
        public IActionResult Index()
        {
            // Return the view with the list of tags
            return View(_tags);
        }

        // GET: /Tags/Create
        public IActionResult Create()
        {
            // Return the view for creating a new tag
            return View();
        }

        // POST: /Tags/Create
        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            // Add the tag to the global list
            _tags.Add(tag);

            // Redirect to the list of tags
            return RedirectToAction("Index");
        }

        // GET: /Tags/Details/{id}
        public IActionResult Details(int id)
        {
            // Find the tag by its ID
            var tag = _tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
            {
                // Return a 404 Not Found if the tag does not exist
                return NotFound();
            }

            // Return the view with the tag details
            return View(tag);
        }
    }
}
