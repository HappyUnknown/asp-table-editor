using ASPTableEditor.Contexts;
using ASPTableEditor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ASPTableEditor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EmployeeListView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");  // or your connection string

            List<Employee> employees = new DatabaseContext(optionsBuilder.Options).Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult EmployeeItemView(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");

            var employee = new DatabaseContext(optionsBuilder.Options).Employees
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee); // Create a corresponding ViewEmployee view
        }

        [HttpGet]
        public IActionResult EmployeeItemCreate()
        {
            var newEmployee = new Employee(); // Create a new instance of the Employee model
            return View(newEmployee); // Pass the new instance to the view
        }
        [HttpPost]
        public IActionResult EmployeeItemCreate(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee); // Return the view with validation errors
            }

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");

            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                Employee brandnewEmployee = new Employee()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    BirthDate = employee.BirthDate,
                    IsMarried = employee.IsMarried,
                    Phone = employee.Phone,
                    Salary = employee.Salary
                };
                context.Employees.Add(brandnewEmployee);

                // Save changes to the database
                context.SaveChanges();
            }

            // Redirect to the employee list view or any other relevant page
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EmployeeItemEdit(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");

            var employee = new DatabaseContext(optionsBuilder.Options).Employees
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee); // Create a corresponding ViewEmployee view
        }
        [HttpPost]
        public IActionResult EmployeeItemEdit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee); // Return the view with validation errors
            }

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");

            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                // Find the existing employee record
                var existingEmployee = context.Employees.FirstOrDefault(e => e.Id == employee.Id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Update the existing employee's properties
                existingEmployee.Id = employee.Id;
                existingEmployee.Name = employee.Name;
                existingEmployee.BirthDate = employee.BirthDate;
                existingEmployee.IsMarried = employee.IsMarried;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Salary = employee.Salary;

                // Save changes to the database
                context.SaveChanges();
            }

            // Redirect to the employee list view or any other relevant page
            return RedirectToAction("EmployeeListView");
        }

        [HttpGet]
        public IActionResult EmployeeItemRemove(int id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");

            var employee = new DatabaseContext(optionsBuilder.Options).Employees
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee); // Create a corresponding ViewEmployee view
        }
        [HttpPost]
        public IActionResult EmployeeItemRemove(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee); // Return the view with validation errors
            }

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");

            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                // Find the existing employee record
                var existingEmployee = context.Employees.FirstOrDefault(e => e.Id == employee.Id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                context.Remove(existingEmployee);

                // Save changes to the database
                context.SaveChanges();
            }

            // Redirect to the employee list view or any other relevant page
            return RedirectToAction("EmployeeListView");
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

                List<Employee> content =
                [
                    // Read the file content
                    new Employee() { BirthDate = DateTime.UtcNow.Date, IsMarried = true, Name = "Patrick", Phone = "+380908087759", Salary = 100 },
                    new Employee() { BirthDate = DateTime.UtcNow.Date, IsMarried = true, Name = "Patrick", Phone = "+380908087759", Salary = 100 },
                    new Employee() { BirthDate = DateTime.UtcNow.Date, IsMarried = true, Name = "Patrick", Phone = "+380908087759", Salary = 100 },
                    new Employee() { BirthDate = DateTime.UtcNow.Date, IsMarried = true, Name = "Patrick", Phone = "+380908087759", Salary = 100 },
                ];
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

                    db.Employees.AddRange(content);

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
