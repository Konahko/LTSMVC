using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTSMVC.Models;
using LTSMVC.Classes.Journals;

namespace LTSMVC.Controllers.BdList
{
    public class JournalExpendablesController : Controller
    {
        private readonly Lts2Context _context;

        public JournalExpendablesController(Lts2Context context)
        {
            _context = context;
        }

        // GET: JournalExpendables
        public async Task<IActionResult> Index(string sortButton, string search, int page, bool isSortButton)
        {
            var countLts2Context = _context.JournalMachines.Count();
            IQueryable<JournalExpendable> lts2Context = _context.JournalExpendables;

            if (isSortButton == true)
            {
                ViewBag.TriggerUserSort = sortButton == "TriggerUser" ? "TriggerUser_dest" : "TriggerUser";
                ViewBag.StateSort = sortButton == "State" ? "State_dest" : "State";
                ViewBag.TimeSort = sortButton == "Time" ? "Time_dest" : "Time";
                ViewBag.ExpendablesItemsSort = sortButton == "Ex_item" ? "Ex_item_dest" : "Ex_item";
            }

            search = !string.IsNullOrEmpty(search) ? search : "";

            switch (sortButton)
            {
                case "TriggerUser":
                    lts2Context = lts2Context.OrderBy(j => j.TriggerUser);
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

                case "Ex_item":
                    lts2Context = lts2Context.OrderBy(j => j.ExpendablesItems);
                    break;

                case "Ex_item_dest":
                    lts2Context = lts2Context.OrderByDescending(j => j.ExpendablesItems);
                    break;

                default:
                    break;
            }

            if (search == "")
            {
                lts2Context = lts2Context.Include(j => j.ExpendablesItems.Expendables)
                    .Skip(page * 25)
                    .Take(25);
            }
            else
            {
                lts2Context = lts2Context.Include(j => j.ExpendablesItems.Expendables)
                .Where(j => EF.Functions.Like(j.ExpendablesItemsId.ToString(), "%" + search + "%")
                || EF.Functions.Like(j.TriggerUser.ToString(), "%" + search + "%")
                || EF.Functions.Like(j.State, "%" + search + "%")
                || EF.Functions.Like(j.ExpendablesItems.Expendables.Name, "%" + search + "%"))
                .Skip(page * 25)
                .Take(25); ; //требуется группировка
            }

            var result = new ExpendablesItemsJournalsView
            {
                Count = countLts2Context / 20 + 1,
                journalExpendables = await lts2Context.ToListAsync(),
                Page = page,
                Search = search,
                SortButton = sortButton
            };

            return View(result);
        }

        // GET: JournalExpendables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalExpendable = await _context.JournalExpendables
                .Include(j => j.ExpendablesItems)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journalExpendable == null)
            {
                return NotFound();
            }

            return View(journalExpendable);
        }

        private bool JournalExpendableExists(int id)
        {
            return _context.JournalExpendables.Any(e => e.Id == id);
        }
    }
}
