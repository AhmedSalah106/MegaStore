using Microsoft.AspNetCore.Mvc;

namespace MegaStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

    }
}
