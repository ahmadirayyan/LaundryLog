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
    public class LauUnitsController : Controller
    {
        private readonly LauContext _context;

        public LauUnitsController(LauContext context)
        {
            _context = context;
        }

        // GET: LauUnits
        public async Task<IActionResult> Index()
        {
            var lauContext = _context.LauUnits.Include(l => l.LauItem).Include(l => l.LauLog);
            return View(await lauContext.ToListAsync());
        }

        // GET: LauUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauUnit = await _context.LauUnits
                .Include(l => l.LauItem)
                .Include(l => l.LauLog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lauUnit == null)
            {
                return NotFound();
            }

            return View(lauUnit);
        }

        // GET: LauUnits/Create
        public IActionResult Create()
        {
            ViewData["LauItemId"] = new SelectList(_context.LauItems, "Id", "Id");
            ViewData["LauLogId"] = new SelectList(_context.LauLogs, "Id", "Id");
            return View();
        }

        // POST: LauUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LauItemId,LauLogId,Quantity,Status,Notes,DateCreated,DateModified")] LauUnit lauUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lauUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LauItemId"] = new SelectList(_context.LauItems, "Id", "Id", lauUnit.LauItemId);
            ViewData["LauLogId"] = new SelectList(_context.LauLogs, "Id", "Id", lauUnit.LauLogId);
            return View(lauUnit);
        }

        // GET: LauUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauUnit = await _context.LauUnits.FindAsync(id);
            if (lauUnit == null)
            {
                return NotFound();
            }
            ViewData["LauItemId"] = new SelectList(_context.LauItems, "Id", "Id", lauUnit.LauItemId);
            ViewData["LauLogId"] = new SelectList(_context.LauLogs, "Id", "Id", lauUnit.LauLogId);
            return View(lauUnit);
        }

        // POST: LauUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LauItemId,LauLogId,Quantity,Status,Notes,DateCreated,DateModified")] LauUnit lauUnit)
        {
            if (id != lauUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lauUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LauUnitExists(lauUnit.Id))
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
            ViewData["LauItemId"] = new SelectList(_context.LauItems, "Id", "Id", lauUnit.LauItemId);
            ViewData["LauLogId"] = new SelectList(_context.LauLogs, "Id", "Id", lauUnit.LauLogId);
            return View(lauUnit);
        }

        // GET: LauUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lauUnit = await _context.LauUnits
                .Include(l => l.LauItem)
                .Include(l => l.LauLog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lauUnit == null)
            {
                return NotFound();
            }

            return View(lauUnit);
        }

        // POST: LauUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lauUnit = await _context.LauUnits.FindAsync(id);
            _context.LauUnits.Remove(lauUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LauUnitExists(int id)
        {
            return _context.LauUnits.Any(e => e.Id == id);
        }
    }
}
