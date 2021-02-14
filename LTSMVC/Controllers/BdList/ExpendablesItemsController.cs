using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;
using QRCoder;
using System.IO;
using System.Text;
using System.Security.Cryptography;

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


        public async Task<IActionResult> DownloadLabel(string size, int id)
        {
            var expendablesItem = await _context.ExpendablesItems
                .Include(e => e.Expendables)
                .FirstOrDefaultAsync(m => m.Id == id);

            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;

            string qrText = "Id=\"" + expendablesItem.Id
                + "\", ExpendaplesId=\"" + expendablesItem.ExpendablesId
                + "\", ExpendableName=\"" + expendablesItem.Expendables.Name
                + "\", Type=\"" + expendablesItem.Expendables.Type
                + "\", PrintDate=\"" + dateTime.ToShortDateString().ToString() + "\"";

            //генерация MD5
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Unicode.GetBytes("Нормальные диски поставь, э ["+qrText+"]");
            string cheksum = BitConverter.ToString(md5.ComputeHash(bytes));
            qrText = qrText + ", Hash =\"" + cheksum + "\"";

            Bitmap image;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData;
            QRCode qrCode;
            Bitmap qrCodeImage;
            Graphics g;
            Font font;

            var stream = new MemoryStream();
            switch (size)
            {
                case "30X20":
                    image = new Bitmap(340, 228);
                    g = Graphics.FromImage(image);

                    qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                    qrCode = new QRCode(qrCodeData);
                    qrCodeImage = qrCode.GetGraphic(4);

                    g.Clear(Color.White);
                    g.DrawImage(qrCodeImage, 72, 27, 200, 200);
                    font = new Font("Arial", 18, FontStyle.Bold);
                    g.DrawString("СЛД 58 Юдино-Казанский", font, Brushes.Black, 18, 6);


                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    font.Dispose();
                    g.Dispose();
                    qrCodeImage.Dispose();
                    qrCode.Dispose();
                    qrCodeData.Dispose();
                    qrGenerator.Dispose();

                    break;
                case "58X40":
                    image = new Bitmap(656, 452);
                    g = Graphics.FromImage(image);


                    qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                    qrCode = new QRCode(qrCodeData);
                    qrCodeImage = qrCode.GetGraphic(6);

                    font = new Font("Arial", 35, FontStyle.Bold);
                    g.Clear(Color.White);
                    g.DrawImage(qrCodeImage, 19, 55, 350, 350);
                    g.DrawString("СЛД 58 Юдино-Казанский", font, Brushes.Black, 19, 6);

                    font = new Font("Arial", 19, FontStyle.Bold);
                    g.DrawString(expendablesItem.Id.ToString(), font, Brushes.Black, 350, 70);
                    g.DrawString(expendablesItem.Expendables.Name.ToString(), font, Brushes.Black, 350, 104);
                    g.DrawString(dateTime.ToShortDateString().ToString(), font, Brushes.Black, 350, 138);

                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    font.Dispose();
                    g.Dispose();
                    qrCodeImage.Dispose();
                    qrCode.Dispose();
                    qrCodeData.Dispose();
                    qrGenerator.Dispose();
                    break;
                default: break;
            }


            //qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

            return File(stream.ToArray(), "application/jpg", expendablesItem.Expendables.Name+" "+ expendablesItem.Id + ".jpg");
        }

    }
}
