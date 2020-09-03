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
    public class LauLogsController : Controller
    {
        private readonly LauContext _context;

        public LauLogsController(LauContext context)
        {
            _context = context;
        }

        // GET: LauLogs
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

            var lauLogs = from ll in _context.LauLogs
                          select ll;
            //.Where(ll => ll.UserId == UserId)

            if (!String.IsNullOrEmpty(searchString))
            {
                lauLogs = lauLogs.Where(ll => ll.DateIn.ToLongDateString().Contains(searchString)
                || ll.DateOut.ToLongDateString().Contains(searchString)
                || ll.Price.ToString().Contains(searchString)
                || ll.Status.ToString().Contains(searchString));
            }

            lauLogs = lauLogs.OrderByDescending(ll => ll.DateIn);

            int pageSize = 3;
            return View(await PaginatedList<LauLog>.CreateAsync(lauLogs.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: LauLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauLog = await _context.LauLogs
                .Include(lu => lu.LauUnits)
                .ThenInclude(li => li.LauItem)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (lauLog == null)
            {
                return NotFound();
            }

            return View(lauLog);
        }

        // GET: LauLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LauLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateIn,DateOut,Price,Status")] LauLog lauLog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(lauLog);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException e)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            
            return View(lauLog);
        }

        // GET: LauLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauLog = await _context.LauLogs.FindAsync(id);
            if (lauLog == null)
            {
                return NotFound();
            }
            return View(lauLog);
        }

        // POST: LauLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauLogToUpdate = await _context.LauLogs.FirstOrDefaultAsync(ll => ll.Id == id);

            if (await TryUpdateModelAsync<LauLog>(
                lauLogToUpdate,
                "",
                ll => ll.DateIn, ll => ll.DateOut, ll => ll.Price, ll => ll.Status))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException e)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                }
            }

            return View(lauLogToUpdate);
        }

        // GET: LauLogs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauLog = await _context.LauLogs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (lauLog == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(lauLog);
        }

        // POST: LauLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lauLog = await _context.LauLogs.FindAsync(id);

            if (lauLog == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.LauLogs.Remove(lauLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool LauLogExists(int id)
        {
            return _context.LauLogs.Any(e => e.Id == id);
        }
    }
}
