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
        public async Task<IActionResult> Index()
        {
            return View(await _context.LauLogs.ToListAsync());
        }

        // GET: LauLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauLog = await _context.LauLogs
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
        public async Task<IActionResult> Create([Bind("Id,DateIn,DateOut,Price,Status")] LauLog lauLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lauLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateIn,DateOut,Price,Status")] LauLog lauLog)
        {
            if (id != lauLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lauLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LauLogExists(lauLog.Id))
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
            return View(lauLog);
        }

        // GET: LauLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauLog = await _context.LauLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lauLog == null)
            {
                return NotFound();
            }

            return View(lauLog);
        }

        // POST: LauLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lauLog = await _context.LauLogs.FindAsync(id);
            _context.LauLogs.Remove(lauLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LauLogExists(int id)
        {
            return _context.LauLogs.Any(e => e.Id == id);
        }
    }
}
