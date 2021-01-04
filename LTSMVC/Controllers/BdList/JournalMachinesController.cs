using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;
using LTSMVC.Classes.Journals;

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
            int CountLts2Context = _context.JournalMachines.Count();
            
            var lts2Context = _context.JournalMachines.Include(j => j.Machine)
                .Skip(CountLts2Context-25).Take(25);
            
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

            var ResultState = journalMachine.State.Split("|");

            var Result = new _MachinesJournal
            {
                
                Id = journalMachine.Id,
                TriggerUser = journalMachine.TriggerUser,
                Machine = journalMachine.Machine,
                MachinesId = journalMachine.MachinesId,
                State = ResultState,
                Time = journalMachine.Time
            };


            
            

            if (journalMachine == null)
            {
                return NotFound();
            }

            return View(Result);
        }

       


        private bool JournalMachineExists(int id)
        {
            return _context.JournalMachines.Any(e => e.Id == id);
        }
    }
}
