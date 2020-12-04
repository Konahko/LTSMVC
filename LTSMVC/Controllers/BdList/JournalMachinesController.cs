using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;

namespace LTSMVC.Controllers
{
    public class JournalMachinesController : Controller
    {
        private readonly Lts2Context _context;

        public JournalMachinesController(Lts2Context context)
        {
            _context = context;
        }

        // GET: JournalMachines
        public async Task<IActionResult> Index()
        {
            var lts2Context = _context.JournalMachines.Include(j => j.Machine);
            return View(await lts2Context.ToListAsync());
        }

        // GET: JournalMachines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalMachine = await _context.JournalMachines
                .Include(j => j.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journalMachine == null)
            {
                return NotFound();
            }

            return View(journalMachine);
        }

        // GET: JournalMachines/Create
        public IActionResult Create()
        {
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber");
            return View();
        }

        // POST: JournalMachines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MachinesId,TriggerUser,State,Time")] JournalMachine journalMachine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journalMachine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", journalMachine.MachinesId);
            return View(journalMachine);
        }

        // GET: JournalMachines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalMachine = await _context.JournalMachines.FindAsync(id);
            if (journalMachine == null)
            {
                return NotFound();
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", journalMachine.MachinesId);
            return View(journalMachine);
        }

        // POST: JournalMachines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MachinesId,TriggerUser,State,Time")] JournalMachine journalMachine)
        {
            if (id != journalMachine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journalMachine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalMachineExists(journalMachine.Id))
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
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", journalMachine.MachinesId);
            return View(journalMachine);
        }

        // GET: JournalMachines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalMachine = await _context.JournalMachines
                .Include(j => j.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journalMachine == null)
            {
                return NotFound();
            }

            return View(journalMachine);
        }

        // POST: JournalMachines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journalMachine = await _context.JournalMachines.FindAsync(id);
            _context.JournalMachines.Remove(journalMachine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JournalMachineExists(int id)
        {
            return _context.JournalMachines.Any(e => e.Id == id);
        }
    }
}
