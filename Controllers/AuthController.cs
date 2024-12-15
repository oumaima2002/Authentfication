namespace Sign.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Sign.Data;
    using Sign.Models;

    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Sign Up
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Message = "Username already exists.";
                return View();
            }

            var userRole = _context.Roles.SingleOrDefault(r => r.Name == "User"); // Récupère le rôle "User"

            var user = new User
            {
                Username = username,
                Password = password, // Vous devriez hacher le mot de passe ici
                RoleId = userRole.Id
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("SignIn");
        }

        // Sign In
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string username, string password)
        {
            var user = _context.Users
                .Where(u => u.Username == username && u.Password == password)
                .Select(u => new { u.Username, u.Role.Name }) // Inclure le rôle
                .SingleOrDefault();

            if (user == null)
            {
                ViewBag.Message = "Invalid credentials.";
                return View();
            }

            // Stocker les informations dans la session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Name); // Correction: stocker le rôle ici

            if (user.Name == "Admin")
                return RedirectToAction("Index", "Admin");


            return RedirectToAction("Index", "Home");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
    }
}