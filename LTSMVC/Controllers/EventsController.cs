using LTSMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTSMVC.Classes.Events;

namespace LTSMVC.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly Lts2Context _context;

        public EventsController(Lts2Context context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .Include(s => s.Staff)
                .Where(s => s.DateTimeOfEvent > DateTime.Now)
                .ToListAsync();


            var oldEvents = await _context.Events
                .Include(s => s.Staff)
                .Where(s => s.DateTimeOfEvent < DateTime.Now && s.DateTimeOfEvent > (DateTime.Now.AddDays(-14)))
                .ToListAsync();

            var eventPage = new EventsPage
            {
                NewEvents = events,
                OldEvents = oldEvents
            };

            return View(eventPage);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //POST Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Events events)
        {
            short user = _context.Staff
                .Where(s => s.ADName == User.Identity.Name)
                .Select(s => s.Id)
                .FirstOrDefault();
            events.EventGenerator = user;
            _context.Add(events);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}
