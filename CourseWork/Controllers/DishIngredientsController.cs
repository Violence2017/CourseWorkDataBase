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
    public class DishIngredientsController : Controller
    {
        private const int PageSize = 20;

        private readonly RestarauntDbContext _context;

        public DishIngredientsController(RestarauntDbContext context)
        {
            _context = context;
        }

        private static IQueryable<DishIngredient> SortItems(IQueryable<DishIngredient> items, DishIngredient.Sort? sort)
        {
            return sort switch
            {
                DishIngredient.Sort.CountAsc => items.OrderBy(o => o.Count),
                DishIngredient.Sort.CountDesc => items.OrderByDescending(o => o.Count),
                DishIngredient.Sort.DishAsc => items.OrderBy(o => o.Dish.Name),
                DishIngredient.Sort.DishDesc => items.OrderByDescending(o => o.Dish.Name),
                DishIngredient.Sort.IngredientAsc => items.OrderBy(o => o.Ingredient.Name),
                DishIngredient.Sort.IngredientDesc => items.OrderByDescending(o => o.Ingredient.Name),
                DishIngredient.Sort.IngredientCountAsc => items.OrderBy(o => o.Ingredient.Count),
                DishIngredient.Sort.IngredientCountDesc => items.OrderByDescending(o => o.Ingredient.Count),
                _ => items
            };
        }

        // GET: DishIngredients
        public async Task<IActionResult> Index(int page = 1, DishIngredient.Sort? sort = null)
        {
            IQueryable<DishIngredient> items = _context.DishIngredients
                .Include(d => d.Dish)
                .Include(d => d.Ingredient);
            var count = items.Count();
            items = SortItems(items, sort);
            items = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new DishIngredientViewModel
            {
                Items = await items.ToListAsync(),
                PageViewModel = new PageViewModel(count, page, PageSize),
                ItemsSort = new DishIngredientSort(sort)
            });
        }

        // GET: DishIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var dishIngredient = await _context.DishIngredients
                .Include(d => d.Dish)
                .Include(d => d.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishIngredient == null) return NotFound();

            return View(dishIngredient);
        }

        // GET: DishIngredients/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name");
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");
            return View();
        }

        // POST: DishIngredients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Count,DishId,IngredientId")] DishIngredient dishIngredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", dishIngredient.DishId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", dishIngredient.IngredientId);
            return View(dishIngredient);
        }

        // GET: DishIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var dishIngredient = await _context.DishIngredients.FindAsync(id);
            if (dishIngredient == null) return NotFound();
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", dishIngredient.DishId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", dishIngredient.IngredientId);
            return View(dishIngredient);
        }

        // POST: DishIngredients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Count,DishId,IngredientId")] DishIngredient dishIngredient)
        {
            if (id != dishIngredient.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishIngredientExists(dishIngredient.Id)) return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", dishIngredient.DishId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", dishIngredient.IngredientId);
            return View(dishIngredient);
        }

        // GET: DishIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dishIngredient = await _context.DishIngredients
                .Include(d => d.Dish)
                .Include(d => d.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishIngredient == null) return NotFound();

            return View(dishIngredient);
        }

        // POST: DishIngredients/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishIngredient = await _context.DishIngredients.FindAsync(id);
            _context.DishIngredients.Remove(dishIngredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishIngredientExists(int id)
        {
            return _context.DishIngredients.Any(e => e.Id == id);
        }
    }
}