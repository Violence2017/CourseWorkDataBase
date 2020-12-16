using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Models.Tables;
using CourseWork.ViewModels;
using CourseWork.ViewModels.Filters;
using CourseWork.ViewModels.Sorts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private const int PageSize = 20;

        private readonly RestarauntDbContext _context;

        public EmployeesController(RestarauntDbContext context)
        {
            _context = context;
        }

        private static IQueryable<Employee> SortItems(IQueryable<Employee> items, Employee.Sort? sort)
        {
            return sort switch
            {
                Employee.Sort.SurnameAsc => items.OrderBy(o => o.Surname),
                Employee.Sort.SurnameDesc => items.OrderByDescending(o => o.Surname),
                Employee.Sort.NameAsc => items.OrderBy(o => o.Name),
                Employee.Sort.NameDesc => items.OrderByDescending(o => o.Name),
                Employee.Sort.MiddleNameAsc => items.OrderBy(o => o.MiddleName),
                Employee.Sort.MiddleNameDesc => items.OrderByDescending(o => o.MiddleName),
                Employee.Sort.PositionAsc => items.OrderBy(o => o.Position),
                Employee.Sort.PositionDesc => items.OrderByDescending(o => o.Position),
                Employee.Sort.EducationAsc => items.OrderBy(o => o.Education),
                Employee.Sort.EducationDesc => items.OrderByDescending(o => o.Education),
                Employee.Sort.PhoneNumberAsc => items.OrderBy(o => o.PhoneNumber),
                Employee.Sort.PhoneNumberDesc => items.OrderByDescending(o => o.PhoneNumber),
                _ => items
            };
        }

        // GET: Employees
        public async Task<IActionResult> Index(string position = null, string education = null, int page = 1,
            Employee.Sort? sort = null)
        {
            IQueryable<Employee> items = _context.Employees;
            if (position != null)
            {
                position = position.ToLower();
                items = items.Where(o => o.Position.ToLower().StartsWith(position));
            }

            if (education != null)
            {
                education = education.ToLower();
                items = items.Where(o => o.Education.ToLower().StartsWith(education));
            }

            var count = items.Count();
            items = SortItems(items, sort);
            items = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new EmployeeViewModel
            {
                Items = await items.ToListAsync(),
                PageViewModel = new PageViewModel(count, page, PageSize),
                ItemsSort = new EmployeeSort(sort),
                ItemsFilter = new EmployeeFilter(position, education)
            });
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,MiddleName,Position,Education,PhoneNumber")]
            Employee employee)
        {
            if (!ModelState.IsValid) return View(employee);
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Surname,Name,MiddleName,Position,Education,PhoneNumber")]
            Employee employee)
        {
            if (id != employee.Id) return NotFound();

            if (!ModelState.IsValid) return View(employee);
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}