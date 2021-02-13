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
    public class ExpendablesController : Controller
    {
        private readonly Lts2Context _context;

        public ExpendablesController(Lts2Context context)
        {
            _context = context;
        }

        // GET: Expendables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Expendables.ToListAsync());
        }

        // GET: Expendables/Details/5
        public async Task<IActionResult> Details(ushort? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expendable = await _context.Expendables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expendable == null)
            {
                return NotFound();
            }

            return View(expendable);
        }

        // GET: Expendables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expendables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Amount")] Expendable expendable)
        {
            if (ModelState.IsValid)
            {
                expendable.Amount = 0;
                _context.Add(expendable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expendable);
        }

        // GET: Expendables/Edit/5
        public async Task<IActionResult> Edit(ushort? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expendable = await _context.Expendables.FindAsync(id);
            if (expendable == null)
            {
                return NotFound();
            }
            return View(expendable);
        }

        // POST: Expendables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ushort id, [Bind("Id,Name,Type")] Expendable expendable)
        {
            if (id != expendable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expendable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpendableExists(expendable.Id))
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
            return View(expendable);
        }

        // GET: Expendables/Delete/5
        public async Task<IActionResult> Delete(ushort? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expendable = await _context.Expendables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expendable == null)
            {
                return NotFound();
            }

            return View(expendable);
        }

        // POST: Expendables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ushort id)
        {
            var expendable = await _context.Expendables.FindAsync(id);
            _context.Expendables.Remove(expendable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpendableExists(ushort id)
        {
            return _context.Expendables.Any(e => e.Id == id);
        }


    }
}
