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

        [HttpPost]
        public IActionResult DisplayFilePath(IFormFile file)
        {
            if (file != null)
            {
                // Get the file name (since full path is not accessible)
                ViewBag.FileName = file.FileName;
            }
            else
            {
                ViewBag.FileName = "No file selected.";
            }

            TempData["Message"] = ViewBag.FileName;
            return RedirectToAction("Index");
        }
    }
}
