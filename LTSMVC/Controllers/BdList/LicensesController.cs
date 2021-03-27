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
    public class LicensesController : Controller
    {
        private readonly Lts2Context _context;

        public LicensesController(Lts2Context context)
        {
            _context = context;
        }

        // GET: Licenses
        public async Task<IActionResult> Index()
        {
            var lts2Context = _context.Licenses.Include(l => l.Machine).Include(l => l.Staff);
            return View(await lts2Context.ToListAsync());
        }

        // GET: Licenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var license = await _context.Licenses
                .Include(l => l.Machine)
                .Include(l => l.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (license == null)
            {
                return NotFound();
            }


            return View(license);
        }

        // GET: Licenses/Create
        public async Task<IActionResult> Create()
        {

            var license = await _context.Licenses
                .Include(l => l.Machine)
                .Include(l => l.Staff)
                .FirstOrDefaultAsync(m => m.Id == 1);
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber");
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName");
            return View();
        }

        // POST: Licenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffId,MachinesId,Pass,Lisence")] License license)
        {
            if (ModelState.IsValid)
            {
                _context.Add(license);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", license.MachinesId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", license.StaffId);
            return View(license);
        }

        // GET: Licenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.Licenses.FindAsync(id);
            if (license == null)
            {
                return NotFound();
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", license.MachinesId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", license.StaffId);
            return View(license);
        }

        // POST: Licenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StaffId,MachinesId,Pass,Lisence")] License license)
        {
            if (id != license.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(license);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenseExists(license.Id))
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
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", license.MachinesId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", license.StaffId);
            return View(license);
        }

        // GET: Licenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.Licenses
                .Include(l => l.Machine)
                .Include(l => l.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (license == null)
            {
                return NotFound();
            }

            return View(license);
        }

        // POST: Licenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var license = await _context.Licenses.FindAsync(id);
            _context.Licenses.Remove(license);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LicenseExists(int id)
        {
            return _context.Licenses.Any(e => e.Id == id);
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
