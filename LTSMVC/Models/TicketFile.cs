using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class TicketFile
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
