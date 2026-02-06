using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

      public IActionResult Index(int page = 1)
{
    int pageSize = 5;

    var employees = _context.Employees
        .OrderBy(e => e.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

    int totalRecords = _context.Employees.Count();
    int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

    ViewBag.CurrentPage = page;
    ViewBag.TotalPages = totalPages;

    return View(employees);
}
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Employee emp)
{
    // Server-side validation = safety net only
    if (!ModelState.IsValid)
    {
        // Do NOT try to manage modal UX here
        // Just return Index with data
        var employees = _context.Employees.ToList();
        return View("Index", employees);
    }

    _context.Employees.Add(emp);
    _context.SaveChanges();
    return RedirectToAction(nameof(Index));
}





       [HttpGet]
public IActionResult Edit(int id)
{
    var emp = _context.Employees.Find(id);
    if (emp == null)
        return NotFound();

    return PartialView("_EditEmployee", emp);
}


        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            _context.Employees.Update(emp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteMultiple(int[] ids)
        {
            var emps = _context.Employees.Where(e => ids.Contains(e.Id)).ToList();
            _context.Employees.RemoveRange(emps);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
