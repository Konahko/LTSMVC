using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;
using LTSMVC.Services;

namespace LTSMVC.Controllers
{
    public class MachinesController : Controller
    {
        private readonly Lts2Context _context;
        private readonly MachineQrGenerator _qrGenerator;


        public MachinesController(Lts2Context context, MachineQrGenerator qrGenerator)
        {
            _context = context;
            _qrGenerator = qrGenerator;
        }

        // GET: Machines
        public async Task<IActionResult> Index()
        {
            var lts2Context =  _context.Machines.Include(m => m.Staff);
            return View(await lts2Context.ToListAsync());
        }

        // GET: Machines/Details/5
        public async Task<IActionResult> Details(short? id)
        {
           if (id == null)
           {
                return NotFound();
           }

            var machine = await _context.Machines
                .Include(m => m.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            string LastUser = "Пользователь не задан";
            if (machine.LastUser != null)
            {
                short LastUserId = (short)machine.LastUser;
                var staff = await _context.Staff
                    .Where(s => s.Id == LastUserId)
                    .Select(s => s.StaffName)
                    .FirstOrDefaultAsync();
                LastUser = staff.ToString();
            }
            ViewData["LastUser"] = LastUser;
            if (machine == null)
            {
                return NotFound();
            }

            return View(machine);
        }

        // GET: Machines/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName");
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffId,Type,Name,InvNumber,Status,Character,Mod,AddInfo,LastUser,Place,Expendables")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", machine.StaffId);
            ViewData["LastUser"] = new SelectList(_context.Staff, "Id", "StaffName", machine.StaffId);
            return View(machine);
        }

        // GET: Machines/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machine = await _context.Machines.FindAsync(id);
            
            if (machine == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", machine.StaffId);
            return View(machine);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,StaffId,Type,Name,InvNumber,Status,Character,Mod,AddInfo,LastUser,Place,Expendables")] Machine machine)
        {
            if (id != machine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineExists(machine.Id))
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
            //ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", machine.StaffId);
            //ViewData["LastUser"] = new SelectList(_context.Staff, "Id", "StaffName", machine.StaffId);
            return View(machine);
        }

        // GET: Machines/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machine = await _context.Machines
                .Include(m => m.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machine == null)
            {
                return NotFound();
            }

            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var machine = await _context.Machines.FindAsync(id);
            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadLabel(QrCodeSize? size, int? id)
        {
            if (!size.HasValue)
            {
                return NotFound();
            }

            var machine = await _context.Machines
            .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null)
            {
                return NotFound(id);
            }
            //var qrSize = (QrCodeSize)size;

            var qrMemoryStream = _qrGenerator.GetQrImageStream(machine, size.Value);

            return File(qrMemoryStream.ToArray(), "application/jpg", machine.Name + " " + machine.Id + ".jpg");
        }

        private bool MachineExists(short id)
        {
            return _context.Machines.Any(e => e.Id == id);
        }
    }
}
