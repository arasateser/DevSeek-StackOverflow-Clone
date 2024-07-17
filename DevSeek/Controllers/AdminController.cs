using Microsoft.AspNetCore.Mvc;

namespace DevSeek.Controllers
{

    public class AdminController : Controller
    {
        // GET: /Admin/
        public IActionResult Index()
        {
            // Return the admin dashboard view
            return View();
        }
    }
}