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
    public class ProvidersController : Controller
    {
        private const int PageSize = 20;

        private readonly RestarauntDbContext _context;

        public ProvidersController(RestarauntDbContext context)
        {
            _context = context;
        }

        private static IQueryable<Provider> SortItems(IQueryable<Provider> items, Provider.Sort? sort)
        {
            return sort switch
            {
                Provider.Sort.NameAsc => items.OrderBy(o => o.Name),
                Provider.Sort.NameDesc => items.OrderByDescending(o => o.Name),
                Provider.Sort.AddressAsc => items.OrderBy(o => o.Address),
                Provider.Sort.AddressDesc => items.OrderByDescending(o => o.Address),
                Provider.Sort.PhoneNumberAsc => items.OrderBy(o => o.PhoneNumber),
                Provider.Sort.PhoneNumberDesc => items.OrderByDescending(o => o.PhoneNumber),
                _ => items
            };
        }

        // GET: Providers
        public async Task<IActionResult> Index(int page = 1, Provider.Sort? sort = null)
        {
            IQueryable<Provider> items = _context.Providers;
            var count = items.Count();
            items = SortItems(items, sort);
            items = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new ProviderViewModel
            {
                Items = await items.ToListAsync(),
                PageViewModel = new PageViewModel(count, page, PageSize),
                ItemsSort = new ProviderSort(sort)
            });
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var provider = await _context.Providers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null) return NotFound();

            return View(provider);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,PhoneNumber")] Provider provider)
        {
            if (!ModelState.IsValid) return View(provider);
            _context.Add(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var provider = await _context.Providers.FindAsync(id);
            if (provider == null) return NotFound();
            return View(provider);
        }

        // POST: Providers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,PhoneNumber")] Provider provider)
        {
            if (id != provider.Id) return NotFound();

            if (!ModelState.IsValid) return View(provider);
            try
            {
                _context.Update(provider);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(provider.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var provider = await _context.Providers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null) return NotFound();

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provider = await _context.Providers.FindAsync(id);
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Providers.Any(e => e.Id == id);
        }
    }
}