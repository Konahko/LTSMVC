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
using LTSMVC.Classes.Tickets;


namespace LTSMVC.Controllers.Jobs.Ticket
{
    public class TicketsListController : Controller
    {

        private readonly Lts2Context _context;

        public TicketsListController(Lts2Context context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(Char typePage, short page, string searchString)       //TypePage M - myTickets A - AllTickets s - Search
        {
            if (User.IsInRole("NEW1HORIZONT\\Eban"))
            {

                if (typePage == '\0')
                    typePage = 'M';
                if (page == 0)
                    page = 1;

                if (typePage == 'M')
                {
                    int worker = _context.Staff
                        .Where(s => s.ADName == User.Identity.Name.ToString())
                        .Select(s => s.Id)
                        .FirstOrDefault();

                    var tickets = await _context.Tickets
                        .Include(t => t.Staff)
                        .Include(t => t.Messages)
                        .Where(t => t.WorkerId == worker)
                        .Take(25)
                        .ToListAsync();

                    var countMyTickets = await _context.Messages
                        .Include(s => s.Ticket)
                        .Where(s => s.IsRead == 0 && s.Ticket.WorkerId == worker)
                        .GroupBy(s => s.TicketId)
                        .CountAsync();

                    var CountAllTickets = await _context.Messages
                        .Where(s => s.IsRead == 0)
                        .GroupBy(s => s.TicketId)
                        .CountAsync();

                    var ticketstolist = new List<TicketsToList>();

                    foreach (var item in tickets)
                    {
                        var lastmessage = _context.Messages
                            .Include(l => l.Staff)
                            .Where(l => l.TicketId == item.Id)
                            .OrderByDescending(l => l.Id)
                            .Take(1)
                            .FirstOrDefault();

                        ticketstolist.Add(new TicketsToList()
                        {
                            Id = item.Id,
                            StaffId = item.StaffId,
                            StaffName = item.Staff.StaffName,
                            Status = item.Status,
                            WorkerId = item.WorkerId,
                            TicketProblem = item.TicketProblem,
                            NameSenderLastMessage = lastmessage.Staff.StaffName,
                            LastMessage = lastmessage.MessageText,
                            DateSend = lastmessage.Date,
                            IsRead = lastmessage.IsRead,
                            IsOnlyFile = lastmessage.IsOnlyFile
                        });
                    }

                    var result = new TicketsList
                    {
                        Tickets = ticketstolist,
                        CountMyTickets = countMyTickets,
                        CountAllTickets = CountAllTickets,
                        TypePage = typePage
                    };
                    return View(result);
                }
                if(typePage =='A')
                {
                    int worker = _context.Staff
                        .Where(s => s.ADName == User.Identity.Name.ToString())
                        .Select(s => s.Id)
                        .FirstOrDefault();

                    var tickets = await _context.Tickets
                        .Include(t => t.Staff)
                        .Take(page*25)
                        .ToListAsync();

                    var ticketstolist = new List<TicketsToList>();

                    foreach(var item in tickets)
                    {
                        var lastMessage = _context.Messages
                            .Include(l => l.Staff)
                            .Where(l => l.TicketId == item.Id)
                            .OrderByDescending(l => l.Id)
                            .Take(1)
                            .FirstOrDefault();

                        var workerName = _context.Staff
                            .Where(w => w.Id == item.WorkerId)
                            .Select(w => w.StaffName)
                            .FirstOrDefault();
                        if (lastMessage.MessageText.Length >= 250)
                            lastMessage.MessageText = lastMessage.MessageText.Substring(0, 250) + "...";

                        ticketstolist.Add(new TicketsToList()
                        {
                            Id = item.Id,
                            StaffId = item.StaffId,
                            StaffName = item.Staff.StaffName,
                            Status = item.Status,
                            WorkerId = item.WorkerId,
                            WorkerName = workerName,
                            TicketProblem = item.TicketProblem,
                            NameSenderLastMessage = lastMessage.Staff.StaffName,
                            NameSenderLastMessageId = lastMessage.FromUser,
                            LastMessage = lastMessage.MessageText,
                            DateSend = lastMessage.Date,
                            IsRead = lastMessage.IsRead,
                            IsOnlyFile = lastMessage.IsOnlyFile
                        });
                    }

                    var selectCountAllTickets = await _context.Messages
                        .Where(s => s.IsRead == 0)
                        .GroupBy(s => s.TicketId)
                        .CountAsync();

                    var countMyTickets = await _context.Messages
                        .Include(s => s.Ticket)
                        .Where(s => s.IsRead == 0 && s.Ticket.WorkerId == worker)
                        .GroupBy(s => s.TicketId)
                        .CountAsync();

                    var result = new TicketsList
                    {
                        CountMyTickets = countMyTickets,
                        CountAllTickets = selectCountAllTickets,
                        TypePage = typePage,
                        Tickets = ticketstolist,
                        PageNum = page
                        
                    };
                    return View(result);
                }
                if (typePage == 'S')
                {
                    int user = _context.Staff
                        .Where(s => s.ADName == User.Identity.Name.ToString())
                        .Select(s => s.Id)
                        .FirstOrDefault();

                    var countMyTickets = await _context.Messages
                        .Include(s => s.Ticket)
                        .Where(s => s.IsRead == 0 && s.Ticket.WorkerId == user)
                        .GroupBy(s => s.TicketId)
                        .CountAsync();

                    var CountAllTickets = await _context.Messages
                        .Where(s => s.IsRead == 0)
                        .GroupBy(s => s.TicketId)
                        .CountAsync();

                    var tickets = await _context.Tickets
                        .Include(s => s.Staff)
                        .Where(s => EF.Functions.Like(s.TicketProblem, "%" + searchString + "%") ||
                        EF.Functions.Like(s.Staff.StaffName, "%" + searchString + "%"))
                        .ToListAsync();

                    var ticketstolist = new List<TicketsToList>();

                    foreach (var item in tickets)
                    {
                        var lastMessage = _context.Messages
                            .Include(l => l.Staff)
                            .Where(l => l.TicketId == item.Id)
                            .OrderByDescending(l => l.Id)
                            .Take(1)
                            .FirstOrDefault();

                        var workerName = _context.Staff
                            .Where(w => w.Id == item.WorkerId)
                            .Select(w => w.StaffName)
                            .FirstOrDefault();
                        if (lastMessage.MessageText.Length >= 250)
                            lastMessage.MessageText = lastMessage.MessageText.Substring(0, 250) + "...";

                        ticketstolist.Add(new TicketsToList()
                        {
                            Id = item.Id,
                            StaffId = item.StaffId,
                            StaffName = item.Staff.StaffName,
                            Status = item.Status,
                            WorkerId = item.WorkerId,
                            WorkerName = workerName,
                            TicketProblem = item.TicketProblem,
                            NameSenderLastMessage = lastMessage.Staff.StaffName,
                            NameSenderLastMessageId = lastMessage.FromUser,
                            LastMessage = lastMessage.MessageText,
                            DateSend = lastMessage.Date,
                            IsRead = lastMessage.IsRead,
                            IsOnlyFile = lastMessage.IsOnlyFile
                        });

                        var result = new TicketsList
                        {
                            CountMyTickets = countMyTickets,
                            CountAllTickets = CountAllTickets,
                            TypePage = typePage,
                            Tickets = ticketstolist,
                            PageNum = page

                        };

                        return View(result);
                    }
                }
                return StatusCode(400);
            }
            else
                return Unauthorized();
        }
    }
}
