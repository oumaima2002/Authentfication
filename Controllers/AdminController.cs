using Microsoft.AspNetCore.Mvc;

namespace Sign.Controllers
{
    public class AdminController : Controller
    {
        // Action Index pour afficher la page d'accueil de l'admin
        public IActionResult Index()
        {
            return View();
        }
    }
}
