using ASPTableEditor.Contexts;
using ASPTableEditor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPTableEditor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeeListView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");  // or your connection string

            List<Employee> employees = new DatabaseContext(optionsBuilder.Options).Employees.ToList();
            return View(employees);
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

                // Initialize DbContextOptionsBuilder for the DatabaseContext
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                optionsBuilder.UseSqlite("Data Source=app.db");  // Use your connection string

                // Create an instance of DatabaseContext
                using (var db = new DatabaseContext(optionsBuilder.Options))
                {
                    db.Database.EnsureCreated();
                    //// Example of adding a new Employee to the database
                    //db.Employees.Add(new Models.Employee()
                    //{
                    //    Id = 1,
                    //    BirthDate = DateTime.Now,
                    //    IsMarried = false,
                    //    Name = "Yuriy",
                    //    Phone = "+380808080808",
                    //    Salary = 8080
                    //});

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Fetch the first employee to display in the response
                    var employeeName = db.Employees.FirstOrDefault()?.Name;

                    // Return a success message with file details and database content
                    return Json(new
                    {
                        message = employeeName,
                        fileName,
                        fileSize,
                        fileType,
                        fileContent = content
                    });
                }
            }

            return Json(new { message = "No file uploaded" });
        }
    }
}
