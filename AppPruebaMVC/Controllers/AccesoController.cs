using Microsoft.AspNetCore.Mvc;

namespace AppPruebaMVC.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
