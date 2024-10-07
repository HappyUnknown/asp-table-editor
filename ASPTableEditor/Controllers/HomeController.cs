using Microsoft.AspNetCore.Mvc;

namespace ASPTableEditor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportTable()
        {
            TempData["Message"] = "Button was clicked!";
            return RedirectToAction("Index");
        }
    }
}
