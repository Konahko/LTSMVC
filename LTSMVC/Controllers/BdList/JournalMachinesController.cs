using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;
using LTSMVC.Classes.Journals;
using System;


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

        public async Task<IActionResult> Index(string sortButton, string search, int page, bool isSortButton)
        {
            var countLts2Context = _context.JournalMachines.Count();
            IQueryable<JournalMachine> lts2Context = _context.JournalMachines;

            if (isSortButton==true)
            {
                ViewBag.TriggerUserSort = sortButton == "TriggerUser" ? "TriggerUser_dest" : "TriggerUser";
                ViewBag.StateSort = sortButton == "State" ? "State_dest" : "State";
                ViewBag.TimeSort = sortButton == "Time" ? "Time_dest" : "Time";
                ViewBag.MachineSort = sortButton == "Machine" ? "Machine_dest" : "Machine";
            }

            search = !string.IsNullOrEmpty(search) ? search : "";

            switch (sortButton)
            {
                case "TriggerUser":
                    lts2Context =lts2Context.OrderBy(j => j.TriggerUser);
                    break;

                case "TriggerUser_dest":
                    lts2Context = lts2Context.OrderByDescending(j => j.TriggerUser);
                    break;

                case "State":
                    lts2Context = lts2Context.OrderBy(j => j.State);
                    break;

                case "State_dest":
                    lts2Context = lts2Context.OrderByDescending(j => j.State);
                    break;

                case "Time":
                    lts2Context = lts2Context.OrderBy(j => j.Time);
                    break;

                case "Time_dest":
                    lts2Context = lts2Context.OrderByDescending(j => j.Time);
                    break;

                case "Machine":
                    lts2Context = lts2Context.OrderBy(j => j.Machine);
                    break;

                case "Machine_dest":
                    lts2Context = lts2Context.OrderByDescending(j => j.Machine);
                    break;

                default:
                    break;
            }

            if (search == "")
            {
                lts2Context = lts2Context.Include(j => j.Machine)
                    .Skip(page * 25)
                    .Take(25);
            }
            else
            {
                lts2Context = lts2Context.Include(j => j.Machine)
                .Where(j => EF.Functions.Like(j.MachinesId.ToString(), "%" + search + "%")
                || EF.Functions.Like(j.TriggerUser.ToString(), "%" + search + "%")
                || EF.Functions.Like(j.State, "%" + search + "%")
                || EF.Functions.Like(j.Machine.InvNumber, "%" + search + "%"))
                .Skip(page * 25)
                .Take(25); ; //требуется группировка
            }

            var result = new MachineJournalsViewModel
            {
                Count = countLts2Context/20+1,
                journalMachines = await lts2Context.ToListAsync(),
                Page = page,
                Search = search,
                SortButton = sortButton
            };

            return View(result);            
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

            if (journalMachine == null)
            {
                return NotFound();
            }

            var resultState = journalMachine.State != null ?
                journalMachine.State.Split("|") :
                Array.Empty<string>();

            var result = new MachineJournalsDetailsViewModel
            {
                Id = journalMachine.Id,
                TriggerUser = journalMachine.TriggerUser,
                Machine = journalMachine.Machine,
                MachinesId = journalMachine.MachinesId,
                State = resultState,
                Time = journalMachine.Time
            };

            return View(result);
        }



        private bool JournalMachineExists(int id) => _context.JournalMachines.Any(e => e.Id == id);
    }
}
