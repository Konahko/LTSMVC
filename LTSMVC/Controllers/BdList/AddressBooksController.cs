using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;

namespace LTSMVC.Views
{
    public class AddressBooksController : Controller
    {
        private readonly Lts2Context _context;

        public AddressBooksController(Lts2Context context)
        {
            _context = context;
        }

        // GET: AddressBooks
        public async Task<IActionResult> Index()
        {
            var lts2Context = _context.AddressBooks.Include(a => a.Staff);
            return View(await lts2Context.ToListAsync());
        }

        // GET: AddressBooks/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBooks
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressBook == null)
            {
                return NotFound();
            }

            return View(addressBook);
        }

        // GET: AddressBooks/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName");
            return View();
        }

        // POST: AddressBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffId,IpNumber,PhoneNumber,Post,Place,Sld")] AddressBook addressBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addressBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", addressBook.StaffId);
            return View(addressBook);
        }

        // GET: AddressBooks/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBooks.FindAsync(id);
            if (addressBook == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", addressBook.StaffId);
            return View(addressBook);
        }

        // POST: AddressBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,StaffId,IpNumber,PhoneNumber,Post,Place,Sld")] AddressBook addressBook)
        {
            if (id != addressBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressBookExists(addressBook.Id))
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
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", addressBook.StaffId);
            return View(addressBook);
        }

        // GET: AddressBooks/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBooks
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressBook == null)
            {
                return NotFound();
            }

            return View(addressBook);
        }

        // POST: AddressBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var addressBook = await _context.AddressBooks.FindAsync(id);
            _context.AddressBooks.Remove(addressBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressBookExists(short id)
        {
            return _context.AddressBooks.Any(e => e.Id == id);
        }
    }
}
