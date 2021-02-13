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
    public class JournalExpendablesController : Controller
    {
        private readonly Lts2Context _context;

        public JournalExpendablesController(Lts2Context context)
        {
            _context = context;
        }

        // GET: JournalExpendables
        public async Task<IActionResult> Index()
        {
            var lts2Context = _context.JournalExpendables.Include(j => j.ExpendablesItems);
            return View(await lts2Context.ToListAsync());
        }

        // GET: JournalExpendables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalExpendable = await _context.JournalExpendables
                .Include(j => j.ExpendablesItems)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journalExpendable == null)
            {
                return NotFound();
            }

            return View(journalExpendable);
        }

        // GET: JournalExpendables/Create
        public IActionResult Create()
        {
            ViewData["ExpendablesItemsId"] = new SelectList(_context.ExpendablesItems, "Id", "Status");
            return View();
        }

        // POST: JournalExpendables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpendablesItemsId,TriggerUser,State,Time")] JournalExpendable journalExpendable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journalExpendable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpendablesItemsId"] = new SelectList(_context.ExpendablesItems, "Id", "Status", journalExpendable.ExpendablesItemsId);
            return View(journalExpendable);
        }

        // GET: JournalExpendables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalExpendable = await _context.JournalExpendables.FindAsync(id);
            if (journalExpendable == null)
            {
                return NotFound();
            }
            ViewData["ExpendablesItemsId"] = new SelectList(_context.ExpendablesItems, "Id", "Status", journalExpendable.ExpendablesItemsId);
            return View(journalExpendable);
        }

        // POST: JournalExpendables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpendablesItemsId,TriggerUser,State,Time")] JournalExpendable journalExpendable)
        {
            if (id != journalExpendable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journalExpendable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalExpendableExists(journalExpendable.Id))
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
            ViewData["ExpendablesItemsId"] = new SelectList(_context.ExpendablesItems, "Id", "Status", journalExpendable.ExpendablesItemsId);
            return View(journalExpendable);
        }

        // GET: JournalExpendables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalExpendable = await _context.JournalExpendables
                .Include(j => j.ExpendablesItems)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journalExpendable == null)
            {
                return NotFound();
            }

            return View(journalExpendable);
        }

        // POST: JournalExpendables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journalExpendable = await _context.JournalExpendables.FindAsync(id);
            _context.JournalExpendables.Remove(journalExpendable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JournalExpendableExists(int id)
        {
            return _context.JournalExpendables.Any(e => e.Id == id);
        }
    }
}
