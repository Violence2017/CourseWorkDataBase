using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Models.Tables;
using CourseWork.ViewModels;
using CourseWork.ViewModels.Filters;
using CourseWork.ViewModels.Sorts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private const int PageSize = 20;

        private readonly RestarauntDbContext _context;

        public OrdersController(RestarauntDbContext context)
        {
            _context = context;
        }

        private static IQueryable<Order> SortItems(IQueryable<Order> items, Order.Sort? sort)
        {
            return sort switch
            {
                Order.Sort.DateAsc => items.OrderBy(o => o.Date),
                Order.Sort.DateDesc => items.OrderByDescending(o => o.Date),
                Order.Sort.TimeAsc => items.OrderBy(o => o.Time),
                Order.Sort.TimeDesc => items.OrderByDescending(o => o.Time),
                Order.Sort.CostAsc => items.OrderBy(o => o.Cost),
                Order.Sort.CostDesc => items.OrderByDescending(o => o.Cost),
                Order.Sort.PaymentAsc => items.OrderBy(o => o.Payment),
                Order.Sort.PaymentDesc => items.OrderByDescending(o => o.Payment),
                Order.Sort.CompletedAsc => items.OrderBy(o => o.Completed),
                Order.Sort.CompletedDesc => items.OrderByDescending(o => o.Completed),
                Order.Sort.CustomerAsc => items.OrderBy(o => o.Customer.Surname).ThenBy(o => o.Customer.Name),
                Order.Sort.CustomerDesc => items.OrderByDescending(o => o.Customer.Surname)
                    .ThenByDescending(o => o.Customer.Name),
                Order.Sort.EmployeeAsc => items.OrderBy(o => o.Employee.Surname).ThenBy(o => o.Employee.Name),
                Order.Sort.EmployeeDesc => items.OrderByDescending(o => o.Employee.Surname)
                    .ThenByDescending(o => o.Employee.Name),
                _ => items
            };
        }

        // GET: Orders
        public async Task<IActionResult> Index(int? selectedEmployeeIndex, int? dishCount, int? selectedDishIndex,
            int? selectedPaymentTypeIndex, int page = 1, Order.Sort? sort = null)
        {
            IQueryable<Order> items = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee);

            if (selectedEmployeeIndex.HasValue && selectedEmployeeIndex.Value != 0)
                items = items.Where(o => o.Employee.Id == selectedEmployeeIndex);

            if (dishCount.HasValue) items = items.Where(o => o.Dishes.Count == dishCount);

            if (selectedDishIndex.HasValue && selectedDishIndex.Value != 0)
                items = items.Where(o => o.Dishes.Any(d => d.Id == selectedDishIndex));

            if (selectedPaymentTypeIndex.HasValue && selectedPaymentTypeIndex.Value != 0)
            {
                var type = (Order.PaymentType) selectedPaymentTypeIndex.Value;
                items = items.Where(o => o.Payment == type);
            }

            var count = items.Count();
            items = SortItems(items, sort);
            items = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new OrderViewModel
            {
                Items = await items.ToListAsync(),
                PageViewModel = new PageViewModel(count, page, PageSize),
                ItemsSort = new OrderSort(sort),
                ItemsFilter = new OrderFilter(await _context.Employees.ToListAsync(), selectedEmployeeIndex, dishCount,
                    await _context.Dishes.ToListAsync(), selectedDishIndex, selectedPaymentTypeIndex)
            });
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null) return NotFound();

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Cost,Payment,Completed,CustomerId,EmployeeId")]
            Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", order.EmployeeId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", order.EmployeeId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Cost,Payment,Completed,CustomerId,EmployeeId")]
            Order order)
        {
            if (id != order.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id)) return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", order.EmployeeId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null) return NotFound();

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}