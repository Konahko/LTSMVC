﻿using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public short StaffId { get; set; }
        public bool? Status { get; set; }
        public short? WorkerId { get; set; }
        public string TicketProblem { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime? DateClose { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}