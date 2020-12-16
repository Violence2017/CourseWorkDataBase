using System;
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
    public class IngredientsController : Controller
    {
        private const int PageSize = 20;

        private readonly RestarauntDbContext _context;

        public IngredientsController(RestarauntDbContext context)
        {
            _context = context;
        }

        private static IQueryable<Ingredient> SortItems(IQueryable<Ingredient> items, Ingredient.Sort? sort)
        {
            return sort switch
            {
                Ingredient.Sort.NameAsc => items.OrderBy(o => o.Name),
                Ingredient.Sort.NameDesc => items.OrderByDescending(o => o.Name),
                Ingredient.Sort.ReleaseDateAsc => items.OrderBy(o => o.ReleaseDate),
                Ingredient.Sort.ReleaseDateDesc => items.OrderByDescending(o => o.ReleaseDate),
                Ingredient.Sort.CountAsc => items.OrderBy(o => o.Count),
                Ingredient.Sort.CountDesc => items.OrderByDescending(o => o.Count),
                Ingredient.Sort.CostAsc => items.OrderBy(o => o.Cost),
                Ingredient.Sort.CostDesc => items.OrderByDescending(o => o.Cost),
                Ingredient.Sort.ExpirationDateAsc => items.OrderBy(o => o.ExpirationDate),
                Ingredient.Sort.ExpirationDateDesc => items.OrderByDescending(o => o.ExpirationDate),
                Ingredient.Sort.ProviderAsc => items.OrderBy(o => o.Provider.Name),
                Ingredient.Sort.ProviderDesc => items.OrderByDescending(o => o.Provider.Name),
                _ => items
            };
        }

        // GET: Ingredients
        public async Task<IActionResult> Index(int? selectedProviderIndex, int? expirationDate, double? cost,
            int page = 1, Ingredient.Sort? sort = null)
        {
            IQueryable<Ingredient> items = _context.Ingredients.Include(i => i.Provider);

            if (selectedProviderIndex.HasValue && selectedProviderIndex.Value != 0)
                items = items.Where(o => o.ProviderId == selectedProviderIndex);

            if (expirationDate.HasValue) items = items.Where(o => o.ExpirationDate == expirationDate);

            if (cost.HasValue) items = items.Where(o => Math.Abs(o.Cost - cost.Value) <= 1);

            var count = items.Count();
            items = SortItems(items, sort);
            items = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new IngredientViewModel
            {
                Items = await items.ToListAsync(),
                PageViewModel = new PageViewModel(count, page, PageSize),
                ItemsSort = new IngredientSort(sort),
                ItemsFilter = new IngredientFilter(await _context.Providers.ToListAsync(), selectedProviderIndex,
                    expirationDate, cost)
            });
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ingredient = await _context.Ingredients
                .Include(i => i.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null) return NotFound();

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name");
            return View();
        }

        // POST: Ingredients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,Count,Cost,ExpirationDate,ProviderId")]
            Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", ingredient.ProviderId);
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null) return NotFound();

            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", ingredient.ProviderId);
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate,Count,Cost,ExpirationDate,ProviderId")]
            Ingredient ingredient)
        {
            if (id != ingredient.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.Id)) return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", ingredient.ProviderId);
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ingredient = await _context.Ingredients
                .Include(i => i.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null) return NotFound();

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}