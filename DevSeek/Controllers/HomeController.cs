using Microsoft.AspNetCore.Mvc;
namespace DevSeek.Controllers
{
    public class HomeController : Controller
    {
        // GET: /
        public IActionResult Index()
        {
            // Return the homepage view
            return View();
        }
    }
}