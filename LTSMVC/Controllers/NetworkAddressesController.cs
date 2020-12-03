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
    public class NetworkAddressesController : Controller
    {
        private readonly Lts2Context _context;

        public NetworkAddressesController(Lts2Context context)
        {
            _context = context;
        }

        // GET: NetworkAddresses
        public async Task<IActionResult> Index()
        {
            var lts2Context = _context.NetworkAdresses.Include(n => n.Machine);
            return View(await lts2Context.ToListAsync());
        }

        // GET: NetworkAddresses/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkAddress = await _context.NetworkAdresses
                .Include(n => n.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (networkAddress == null)
            {
                return NotFound();
            }

            return View(networkAddress);
        }

        // GET: NetworkAddresses/Create
        public IActionResult Create()
        {
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber");
            return View();
        }

        // POST: NetworkAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Network,IpAddress,MachinesId,Mac,AddressType")] NetworkAddress networkAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(networkAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", networkAddress.MachinesId);
            return View(networkAddress);
        }

        // GET: NetworkAddresses/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkAddress = await _context.NetworkAdresses.FindAsync(id);
            if (networkAddress == null)
            {
                return NotFound();
            }
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", networkAddress.MachinesId);
            return View(networkAddress);
        }

        // POST: NetworkAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Network,IpAddress,MachinesId,Mac,AddressType")] NetworkAddress networkAddress)
        {
            if (id != networkAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(networkAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetworkAddressExists(networkAddress.Id))
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
            ViewData["MachinesId"] = new SelectList(_context.Machines, "Id", "InvNumber", networkAddress.MachinesId);
            return View(networkAddress);
        }

        // GET: NetworkAddresses/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkAddress = await _context.NetworkAdresses
                .Include(n => n.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (networkAddress == null)
            {
                return NotFound();
            }

            return View(networkAddress);
        }

        // POST: NetworkAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var networkAddress = await _context.NetworkAdresses.FindAsync(id);
            _context.NetworkAdresses.Remove(networkAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NetworkAddressExists(short id)
        {
            return _context.NetworkAdresses.Any(e => e.Id == id);
        }
    }
}
