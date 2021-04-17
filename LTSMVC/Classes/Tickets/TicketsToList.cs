using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Classes.Tickets
{
    public class TicketsToList
    {
        public int Id { get; set; }
        public short StaffId { get; set; }
        public string StaffName { get; set; }
        public bool? Status { get; set; }
        public short? WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string TicketProblem { get; set; }
        public string NameSenderLastMessage { get; set; }
        public short NameSenderLastMessageId { get; set; }
        public string LastMessage { get; set; }
        public DateTime DateSend { get; set; }
        public sbyte IsRead { get; set; }
        public sbyte IsOnlyFile { get; set; }
    }
}
