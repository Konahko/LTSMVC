using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;
using System.Web.Mvc.Ajax;



namespace LTSMVC.Controllers
{
    public class MachinesConnectsController : Controller
    {
        private readonly Lts2Context _context;

        public MachinesConnectsController(Lts2Context context)
        {
            _context = context;
        }

        // GET: MachinesConnects
        public async Task<IActionResult> Index()
        {
            var lts2Context = _context.MachinesConnects.Include(m => m.Machines);
            return View(await lts2Context.ToListAsync());
        }

        // GET: MachinesConnects/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machinesConnect = await _context.MachinesConnects
                .Include(m => m.Machines)
                .FirstOrDefaultAsync(m => m.id == id);


            if (machinesConnect == null)
            {
                return NotFound();
            }

            return View(machinesConnect);
        }

        // GET: MachinesConnects/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "Name");

            var machinesConnect = await _context.MachinesConnects
                .Include(m => m.Machines)
                .FirstOrDefaultAsync(m => m.id == 1);

            return View(machinesConnect);
        }

        // POST: MachinesConnects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,MachinesId,Login,Pass,IsAdmin")] MachinesConnect machinesConnect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machinesConnect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", machinesConnect.MachinesId);
            return View(machinesConnect);
        }

        // GET: MachinesConnects/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machinesConnect = await _context.MachinesConnects.FindAsync(id);
            if (machinesConnect == null)
            {
                return NotFound();
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "Name", machinesConnect.MachinesId);
            return View(machinesConnect);
        }

        // POST: MachinesConnects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("id,MachinesId,Login,Pass,IsAdmin")] MachinesConnect machinesConnect)
        {
            if (id != machinesConnect.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machinesConnect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachinesConnectExists(machinesConnect.id))
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
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "Name", machinesConnect.MachinesId);
            return View(machinesConnect);
        }

        // GET: MachinesConnects/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machinesConnect = await _context.MachinesConnects
                .Include(m => m.Machines)
                .FirstOrDefaultAsync(m => m.id == id);
            if (machinesConnect == null)
            {
                return NotFound();
            }

            return View(machinesConnect);
        }

        // POST: MachinesConnects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var machinesConnect = await _context.MachinesConnects.FindAsync(id);
            _context.MachinesConnects.Remove(machinesConnect);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachinesConnectExists(short id)
        {
            return _context.MachinesConnects.Any(e => e.id == id);
        }


        // Обновление блока с информацией о машине


        [HttpGet]
        public async Task<ActionResult> InfoUpdate(int Id)
        {
            var machinesConnect = await _context.Machines
                .SingleOrDefaultAsync(m => m.Id == Id);
            return Ok(machinesConnect);
        }

    }


  
}
