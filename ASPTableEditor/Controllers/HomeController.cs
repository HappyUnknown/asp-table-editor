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

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Optionally process file properties
                var fileName = Path.GetFileName(file.FileName);
                var fileSize = file.Length;
                var fileType = file.ContentType;

                // Read the file content
                string content;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    content = await reader.ReadToEndAsync();
                }

                // Return a success message with file details and content
                return Json(new
                {
                    message = "File uploaded successfully",
                    fileName,
                    fileSize,
                    fileType,
                    fileContent = content // Include the file content in the response
                });
            }

            return Json(new { message = "No file uploaded" });
        }
    }
}
