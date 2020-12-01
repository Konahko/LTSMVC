using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class TicketFile
    {
        public int IdMessageFile { get; set; }
        public int TicketIdTicket { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }

        public virtual Ticket TicketIdTicketNavigation { get; set; }
    }
}
