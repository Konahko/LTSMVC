using LTSMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LTSMVC.Classes.Home;

namespace LTSMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Lts2Context db;
        private readonly Lts2Context _context;

        public HomeController(ILogger<HomeController> logger, Lts2Context context)
        {
            _context = context;
            _logger = logger;
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("NEW1HORIZONT\\Eban"))
            {
                var ticket = await _context.Tickets
                    .Include(s => s.Staff)
                    .Take(4)
                    .Where(s => s.WorkerId == null)
                    .ToListAsync();



                var task = await _context.StaffsTasks
                    .Include(s => s.Staff)
                    .Include(s => s.Tasks)
                    .Where(s => s.Staff.ADName == User.Identity.Name.ToString() && s.Status==0)
                    .Take(4)
                    .ToListAsync();

                var result = new HomeIndex
                {
                    Tickets = ticket,
                    CountNewTickets = (await _context.Tickets
                    .Where(s => s.WorkerId == null)
                    .ToListAsync()).Count,
                    CountActiveTikets = (await _context.Tickets
                    .Where(s => s.WorkerId == 1)
                    .ToListAsync()).Count,
                    StaffsTasks = task,
                    CountActiveTasks = (await _context.StaffsTasks
                    .Include(s => s.Staff)
                    .Where(s => s.Staff.ADName == User.Identity.Name.ToString() && s.Status == 0)
                    .ToListAsync()).Count
                };

                foreach (var item in result.StaffsTasks)
                {
                    if (item.Tasks.Task.Length > 60)
                        item.Tasks.Task = item.Tasks.Task.Substring(0, 60) + "...";
                }

                return View(result);
            }
      
            return Unauthorized();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        // Tools________________________________

        public IActionResult BdList()
        {
            return View();
        }

        
        public async Task<IActionResult> User_listAsync()
        {
            var items = await db.Staff.Include(x => x.AddressBooks).AsNoTracking().ToListAsync();
            var item = items.FirstOrDefault();
            item.AddressBooks.Where(x => x.StaffId == 1);
            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
