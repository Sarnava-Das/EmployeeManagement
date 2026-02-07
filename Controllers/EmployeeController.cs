using Microsoft.AspNetCore.Mvc;                  
using EmployeeManagement.Data;                  
using EmployeeManagement.Models;                

namespace EmployeeManagement.Controllers
{
    //Dependency injection of ApplicationDbContext to interact with the database
     public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context; // DbContext instance

        public EmployeeController(ApplicationDbContext context)// Constructor to inject DbContext i.e whatever is registered in the application we can access that
        {
            _context = context;                  // Inject DbContext
        }

        public IActionResult Index(int page = 1)//IActionResult is for returning anything i.e superclass of all returns like views,pages
        {
            int pageSize = 5;                    
                 
    // IEnumerable<Employee> employee = _context.Employees.ToList(); we can also use Inumerable instead of var  
                
            var employees = _context.Employees   // Fetch paginated employees and convert to list
                .OrderBy(e => e.Id)// Order by Id for consistent pagination
                .Skip((page - 1) * pageSize)// Skip records for previous pages
                .Take(pageSize)// Take records for current page
                .ToList();

            int totalRecords = _context.Employees.Count(); 
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize); 

            ViewBag.CurrentPage = page;           // Current page number
            ViewBag.TotalPages = totalPages;     // Total pages for pagination

            return View(employees);               // Return employee list view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (!ModelState.IsValid)              // Server-side validation check
            {
                var employees = _context.Employees.ToList(); // Reload employee list
                return View("Index", employees);  // Return Index view on validation failure
            }

            _context.Employees.Add(emp);          // Add new employee
            _context.SaveChanges();               // Save to database
            return RedirectToAction(nameof(Index)); // Redirect to Index
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var emp = _context.Employees.Find(id); // Find employee by Id
            if (emp == null)
                return NotFound();                // Return 404 if not found

            return PartialView("_EditEmployee", emp); // Load edit modal
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            _context.Employees.Update(emp);       // Update employee
            _context.SaveChanges();               
            return RedirectToAction("Index");     
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var emp = _context.Employees.Find(id); 
            if (emp != null)
            {
                _context.Employees.Remove(emp);   // Remove employee
                _context.SaveChanges();           
            }
            return RedirectToAction("Index");    
        }

        [HttpPost]
        public IActionResult DeleteMultiple(int[] ids)
        {
            var emps = _context.Employees.Where(e => ids.Contains(e.Id)).ToList(); // Fetch selected employees list
            _context.Employees.RemoveRange(emps); // Delete multiple employees using the list 
            _context.SaveChanges();               
            return RedirectToAction("Index");     
        }
    }
}
