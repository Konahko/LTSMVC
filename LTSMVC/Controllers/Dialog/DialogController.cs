using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTSMVC.Models;
using LTSMVC.Classes.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace LTSMVC.Controllers.Tickets
{
    public class DialogController : Controller
    {
        private readonly Lts2Context _context;

        public DialogController(Lts2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int ticketId)
        {
            //POST Isreaded Dialog
            var message = await _context.Messages
                .Where(s => s.TicketId == ticketId && s.IsRead == 0)
                .ToListAsync();
            foreach(var item in message)
            {
                item.IsRead = 1;
                _context.Update(item);
                await _context.SaveChangesAsync();
            }



            //GET Dialog
            var dialogInform = await _context.Tickets
                .Include(d => d.Staff)
                .Where(d => d.Id == ticketId)
                .FirstOrDefaultAsync();

            var partner = _context.Staff
                .Where(s => s.Id == dialogInform.WorkerId)
                .Select(s => s.StaffName)
                .FirstOrDefault();

            ViewData["Partner"] = new SelectList(_context.Staff.Where(s => s.AdminU==true), "Id", "StaffName", dialogInform.WorkerId);

            var messages = await _context.Messages
                .Include(m => m.MessageFiles)
                .Where(m => m.TicketId == ticketId)
                .ToListAsync();

            var dialogMessages = new List<DialogMessages>();
            foreach(var item in messages)
            {
                var files = new List<MessageFile>();
                foreach (var seconditem in item.MessageFiles)
                {
                    files.Add(new MessageFile()
                    {
                        Id=seconditem.Id,
                        DataType = seconditem.DataType,
                        MessageId = seconditem.MessageId,
                        Messages = seconditem.Messages,
                        Name = seconditem.Name
                    });
                }

                dialogMessages.Add(new DialogMessages()
                {
                    Date = item.Date,
                    FromUser = item.FromUser,
                    FromUserName = _context.Staff
                    .Where(s => s.Id == item.FromUser)
                    .Select(s => s.StaffName)
                    .FirstOrDefault(),
                    Id=item.Id,
                    IsOnlyFile = item.IsOnlyFile,
                    MessageText = item.MessageText,
                    messageFiles = files
                }) ;
                    
            }

            var result = new Dialog
            {
                Id =dialogInform.Id,
                DateOpen = dialogInform.DateOpen,
                IsOpen = dialogInform.Status,
                Partner = partner,
                PartnerId = dialogInform.WorkerId == null ? (short)0 : (short)dialogInform.WorkerId,
                Subj = dialogInform.TicketProblem,
                User = dialogInform.Staff.StaffName,
                UserId = dialogInform.StaffId,
                DialogMessages = dialogMessages
            };


            return View(result);
        }

        //POST AdminMessage
        //[Authorize]
        public IActionResult SendAdminMessage(string id, string text)
        {
            if (User.IsInRole("NEW1HORIZONT\\Eban"))
            {
                int ticketId = int.Parse(id);

                short user = _context.Staff
                    .Where(s => s.ADName == User.Identity.Name)
                    .Select(s => s.Id)
                    .FirstOrDefault();

                int sended = 0;

                do
                {
                    string substring;
                    if((sended*5000)+5000<text.Length)
                        substring = text.Substring(sended * 5000, 5000);
                    else
                        substring= text.Substring(sended * 5000, text.Length - (sended * 5000));
                    var message = new Message
                    {
                        TicketId = ticketId,
                        Date = DateTime.Now,
                        FromUser = user,
                        IsOnlyFile = 0,
                        IsRead = 1,
                        MessageText = substring                       
                    };
                    _context.Add(message);
                    _context.SaveChanges();
                    sended++;
                }
                while (sended < (float)((float)text.Length / 1000));
                return StatusCode(201);
            }
            else
            {
                Response.StatusCode = 404;
                return View();
            }
        }

        //[Authorize]
        public IActionResult ChangeStatus(string id, string status)
        {
            if (User.IsInRole("NEW1HORIZONT\\Eban"))
            {
                int ticketId = int.Parse(id);
                var ticket = _context.Tickets
                    .Where(t => t.Id == ticketId)
                    .FirstOrDefault();

                switch(ticket.Status)
                {
                    case true:
                        ticket.Status = false;
                        ticket.DateClose = DateTime.Now;
                        break;
                    case false:
                        ticket.Status = true;
                        ticket.DateClose = null;
                        break;
                    default:
                        break;
                }
                _context.Update(ticket);
                _context.SaveChanges();
                return StatusCode(201);
            }
            return Unauthorized();

        }

        //[Authorize]
        public IActionResult GetWorkerId(int ticketId)
        {
            if (User.IsInRole("NEW1HORIZONT\\Eban"))
            {
                var result = _context.Tickets
                    .Where(t => t.Id == ticketId)
                    .Select(t => t.WorkerId)
                    .FirstOrDefault();
                return Ok(result);
            }
            else
            return Unauthorized();
            
        }

        //[Authorize]
        public IActionResult ChangePartner(int id, short workerId)
        {
            if (User.IsInRole("NEW1HORIZONT\\Eban"))
            {
                var ticket = _context.Tickets
                   .Where(t => t.Id == id)
                   .FirstOrDefault();
                ticket.WorkerId = workerId;
                _context.Update(ticket);
                _context.SaveChanges();
                return StatusCode(201);
            }
            else
            return Unauthorized();

        }
    }
}
