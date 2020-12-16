using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Models.Tables;
using CourseWork.ViewModels;
using CourseWork.ViewModels.Sorts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Controllers
{
    [Authorize]
    public class DishesController : Controller
    {
        private const int PageSize = 20;

        private readonly RestarauntDbContext _context;

        public DishesController(RestarauntDbContext context)
        {
            _context = context;
        }

        private static IQueryable<Dish> SortItems(IQueryable<Dish> items, Dish.Sort? sort)
        {
            return sort switch
            {
                Dish.Sort.NameAsc => items.OrderBy(o => o.Name),
                Dish.Sort.NameDesc => items.OrderByDescending(o => o.Name),
                Dish.Sort.CostAsc => items.OrderBy(o => o.Cost),
                Dish.Sort.CostDesc => items.OrderByDescending(o => o.Cost),
                Dish.Sort.CookingTimeAsc => items.OrderBy(o => o.CookingTime),
                Dish.Sort.CookingTimeDesc => items.OrderByDescending(o => o.CookingTime),
                Dish.Sort.OrderAsc => items.OrderBy(o => o.Order.Date.Date).ThenBy(o => o.Order.Time.TimeOfDay),
                Dish.Sort.OrderDesc => items.OrderByDescending(o => o.Order.Date.Date)
                    .ThenByDescending(o => o.Order.Time.TimeOfDay),
                _ => items
            };
        }

        // GET: Dishes
        public async Task<IActionResult> Index(int page = 1, Dish.Sort? sort = null)
        {
            IQueryable<Dish> items = _context.Dishes
                .Include(d => d.Order);
            var count = items.Count();
            items = SortItems(items, sort);
            items = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new DishViewModel
            {
                Items = await items.ToListAsync(),
                PageViewModel = new PageViewModel(count, page, PageSize),
                ItemsSort = new DishSort(sort)
            });
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var dish = await _context.Dishes
                .Include(d => d.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null) return NotFound();

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DateTime");
            return View();
        }

        // POST: Dishes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cost,CookingTime,OrderId")]
            Dish dish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DateTime", dish.OrderId);
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null) return NotFound();
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DateTime", dish.OrderId);
            return View(dish);
        }

        // POST: Dishes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cost,CookingTime,OrderId")]
            Dish dish)
        {
            if (id != dish.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.Id)) return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DateTime", dish.OrderId);
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dish = await _context.Dishes
                .Include(d => d.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null) return NotFound();

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }
}