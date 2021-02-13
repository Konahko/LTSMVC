using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;

namespace LTSMVC.Controllers.BdList
{
    public class ExpendablesItemsController : Controller
    {
        private readonly Lts2Context _context;

        public ExpendablesItemsController(Lts2Context context)
        {
            _context = context;
        }

        // GET: ExpendablesItems
        public async Task<IActionResult> Index()
        {
            var lts2Context = _context.ExpendablesItems.Include(e => e.Expendables).Include(e => e.Staff);
            return View(await lts2Context.ToListAsync());
        }

        // GET: ExpendablesItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expendablesItem = await _context.ExpendablesItems
                .Include(e => e.Expendables)
                .Include(e => e.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expendablesItem == null)
            {
                return NotFound();
            }

            return View(expendablesItem);
        }

        // GET: ExpendablesItems/Create
        public IActionResult Create()
        {
            ViewData["ExpendablesId"] = new SelectList(_context.Expendables, "Id", "Name");
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName");
            return View();
        }

        // POST: ExpendablesItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpendablesId,Status,StaffId")] ExpendablesItem expendablesItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expendablesItem);
                await _context.SaveChangesAsync();
                var lastElement = await _context.ExpendablesItems
                    .Where(e => e.Status == expendablesItem.Status &&
                    e.StaffId == expendablesItem.StaffId &&
                    e.ExpendablesId == expendablesItem.ExpendablesId)
                    .OrderBy(e => e.Id)
                    .LastAsync();
                return RedirectToAction("Details", new { id=lastElement.Id });
            }
            ViewData["ExpendablesId"] = new SelectList(_context.Expendables, "Id", "Name", expendablesItem.ExpendablesId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", expendablesItem.StaffId);   
            return View(expendablesItem);
        }

        // GET: ExpendablesItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expendablesItem = await _context.ExpendablesItems.FindAsync(id);
            if (expendablesItem == null)
            {
                return NotFound();
            }
            ViewData["ExpendablesId"] = new SelectList(_context.Expendables, "Id", "Name", expendablesItem.ExpendablesId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", expendablesItem.StaffId);
            return View(expendablesItem);
        }

        // POST: ExpendablesItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpendablesId,Status,StaffId")] ExpendablesItem expendablesItem)
        {
            if (id != expendablesItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expendablesItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpendablesItemExists(expendablesItem.Id))
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
            ViewData["ExpendablesId"] = new SelectList(_context.Expendables, "Id", "Name", expendablesItem.ExpendablesId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", expendablesItem.StaffId);
            return View(expendablesItem);
        }

        // GET: ExpendablesItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expendablesItem = await _context.ExpendablesItems
                .Include(e => e.Expendables)
                .Include(e => e.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expendablesItem == null)
            {
                return NotFound();
            }

            return View(expendablesItem);
        }

        // POST: ExpendablesItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expendablesItem = await _context.ExpendablesItems.FindAsync(id);
            _context.ExpendablesItems.Remove(expendablesItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpendablesItemExists(int id)
        {
            return _context.ExpendablesItems.Any(e => e.Id == id);
        }


    }
}
