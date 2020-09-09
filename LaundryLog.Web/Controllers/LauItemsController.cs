using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaundryLog.Web.Data;
using LaundryLog.Web.Models;

namespace LaundryLog.Web.Controllers
{
    public class LauItemsController : Controller
    {
        private readonly LauContext _context;

        public LauItemsController(LauContext context)
        {
            _context = context;
        }

        // GET: LauItems
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var lauItems = from li in _context.LauItems
                           select li;

            if (!String.IsNullOrEmpty(searchString))
            {
                lauItems = lauItems.Where(li => li.Category.Contains(searchString)
                || li.Color.Contains(searchString)
                || li.Brand.Contains(searchString));
            }

            int pageSize = 5;
            return View(await PaginatedList<LauItem>.CreateAsync(lauItems.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: LauItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauItem = await _context.LauItems
                .Include(lu => lu.LauUnits)
                .ThenInclude(ll => ll.LauLog)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (lauItem == null)
            {
                return NotFound();
            }

            return View(lauItem);
        }

        // GET: LauItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LauItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Color,Brand,Price,Description,Status,DateBought,DateCreated,DateModified")] LauItem lauItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lauItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lauItem);
        }

        // GET: LauItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauItem = await _context.LauItems.FindAsync(id);
            if (lauItem == null)
            {
                return NotFound();
            }
            return View(lauItem);
        }

        // POST: LauItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Color,Brand,Price,Description,Status,DateBought,DateCreated,DateModified")] LauItem lauItem)
        {
            if (id != lauItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lauItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LauItemExists(lauItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lauItem);
        }

        // GET: LauItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauItem = await _context.LauItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lauItem == null)
            {
                return NotFound();
            }

            return View(lauItem);
        }

        // POST: LauItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lauItem = await _context.LauItems.FindAsync(id);
            _context.LauItems.Remove(lauItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LauItemExists(int id)
        {
            return _context.LauItems.Any(e => e.Id == id);
        }
    }
}
