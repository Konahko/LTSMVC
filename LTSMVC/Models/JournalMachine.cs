using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class JournalMachine
    {
        public int IdJournalMachines { get; set; }
        public short MachinesIdMachines { get; set; }
        public int? TrigerUser { get; set; }
        public string State { get; set; }
        public DateTime? Time { get; set; }

        public virtual Machine MachinesIdMachinesNavigation { get; set; }
    }
}
