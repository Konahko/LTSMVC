using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketFiles = new HashSet<TicketFile>();
        }

        public int IdTicket { get; set; }
        public short UserId { get; set; }
        public bool? Status { get; set; }
        public string TicketProblem { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime? DateClose { get; set; }

        public virtual staff User { get; set; }
        public virtual ICollection<TicketFile> TicketFiles { get; set; }
    }
}
