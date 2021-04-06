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
    public class RemoveControlsController : Controller
    {
        private readonly Lts2Context _context;

        public RemoveControlsController(Lts2Context context)
        {
            _context = context;
        }

        // GET: RemoveControls
        public async Task<IActionResult> Index()
        {
            var lts2Context = _context.RemoveControls.Include(r => r.Machines);
            return View(await lts2Context.ToListAsync());
        }

        // GET: RemoveControls/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var removeControl = await _context.RemoveControls
                .Include(r => r.Machines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (removeControl == null)
            {
                return NotFound();
            }

            return View(removeControl);
        }

        // GET: RemoveControls/Create
        public IActionResult Create()
        {
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber");
            var machine = _context.RemoveControls
                .Include(s => s.Machines)
                .Where(s => s.MachinesId == 1)
                .FirstOrDefault();
            return View(machine);
        }

        // POST: RemoveControls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MachinesId,AnyDesk,AnyDeskPassword,TeamViewer,TeamViewerPassword,AmmyAdmin,AmmyAdminPassword,Rdp")] RemoveControl removeControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(removeControl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", removeControl.MachinesId);
            return View(removeControl);
        }

        // GET: RemoveControls/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var removeControl = await _context.RemoveControls.FindAsync(id);
            if (removeControl == null)
            {
                return NotFound();
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", removeControl.MachinesId);
            return View(removeControl);
        }

        // POST: RemoveControls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,MachinesId,AnyDesk,AnyDeskPassword,TeamViewer,TeamViewerPassword,AmmyAdmin,AmmyAdminPassword,Rdp")] RemoveControl removeControl)
        {
            if (id != removeControl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(removeControl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RemoveControlExists(removeControl.Id))
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
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", removeControl.MachinesId);
            return View(removeControl);
        }

        // GET: RemoveControls/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var removeControl = await _context.RemoveControls
                .Include(r => r.Machines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (removeControl == null)
            {
                return NotFound();
            }

            return View(removeControl);
        }

        // POST: RemoveControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var removeControl = await _context.RemoveControls.FindAsync(id);
            _context.RemoveControls.Remove(removeControl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RemoveControlExists(short id)
        {
            return _context.RemoveControls.Any(e => e.Id == id);
        }


        [HttpGet]
        public async Task<ActionResult> InfoUpdate(int Id)
        {
            var machinesConnect = await _context.Machines
                .SingleOrDefaultAsync(m => m.Id == Id);
            return Ok(machinesConnect);
        }
    }
}
