using Microsoft.AspNetCore.Mvc;
using DevSeek.Models; // Include the namespace for LoginViewModel
namespace DevSeek.Controllers
{
    public class UsersController : Controller
    {
        // In-memory user storage
        private static List<User> _users = new List<User>();

        // GET: /Users/
        public IActionResult Index()
        {
            // Return the view with the list of users
            return View(_users);
        }

        // GET: /Users/Register
        public IActionResult Register()
        {
            // Return the view for user registration
            return View();
        }

        // POST: /Users/Register
        [HttpPost]
        public IActionResult Register(User user)
        {
            // Check if the username is already taken
            if (_users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists");
                return View();
            }

            // Add the user to the global list and redirect to login
            _users.Add(user);
            return RedirectToAction("Login");
        }

        // GET: /Users/Login
        public IActionResult Login()
        {
            // Return the view for user login
            return View();
        }

        // POST: /Users/Login
        [HttpPost]
        public IActionResult Login(LoginView login)
        {
            // Check if the username and password match
            var user = _users.FirstOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View();
            }

            // Store username in session and redirect to homepage
            HttpContext.Session.SetString("UserName", user.UserName);
            return RedirectToAction("Index", "Home");
        }

        // POST: /Users/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            // Clear the session and redirect to homepage
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}