using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Classes.Journals
{
    public class _MachinesJournal
    {
        public int Id;
        public int? TriggerUser;
        public LTSMVC.Models.Machine Machine;
        public int MachinesId;
        public string[] State;
        public DateTime? Time;
    }
}
