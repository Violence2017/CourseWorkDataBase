using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Models.Tables;
using CourseWork.ViewModels;
using CourseWork.ViewModels.Sorts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private const int PageSize = 20;

        private readonly RestarauntDbContext _context;

        public CustomersController(RestarauntDbContext context)
        {
            _context = context;
        }

        private static IQueryable<Customer> SortItems(IQueryable<Customer> items, Customer.Sort? sort)
        {
            return sort switch
            {
                Customer.Sort.SurnameAsc => items.OrderBy(o => o.Surname),
                Customer.Sort.SurnameDesc => items.OrderByDescending(o => o.Surname),
                Customer.Sort.NameAsc => items.OrderBy(o => o.Name),
                Customer.Sort.NameDesc => items.OrderByDescending(o => o.Name),
                Customer.Sort.MiddleNameAsc => items.OrderBy(o => o.MiddleName),
                Customer.Sort.MiddleNameDesc => items.OrderByDescending(o => o.MiddleName),
                Customer.Sort.PhoneNumberAsc => items.OrderBy(o => o.PhoneNumber),
                Customer.Sort.PhoneNumberDesc => items.OrderByDescending(o => o.PhoneNumber),
                _ => items
            };
        }

        // GET: Customers
        public async Task<IActionResult> Index(int page = 1, Customer.Sort? sort = null)
        {
            IQueryable<Customer> items = _context.Customers;
            var count = items.Count();
            items = SortItems(items, sort);
            items = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new CustomerViewModel
            {
                Items = await items.ToListAsync(),
                PageViewModel = new PageViewModel(count, page, PageSize),
                ItemsSort = new CustomerSort(sort)
            });
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,MiddleName,PhoneNumber")]
            Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,MiddleName,PhoneNumber")]
            Customer customer)
        {
            if (id != customer.Id) return NotFound();

            if (!ModelState.IsValid) return View(customer);
            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}