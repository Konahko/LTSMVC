using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Machine
    {
        public Machine()
        {
            JournalMachines = new HashSet<JournalMachine>();
            Licenses = new HashSet<License>();
            MachinesConnects = new HashSet<MachinesConnect>();
            RemoveControls = new HashSet<RemoveControl>();
        }

        public short IdMachines { get; set; }
        public short UserId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string InvNumber { get; set; }
        public string Status { get; set; }
        public string Charecter { get; set; }
        public string Mod { get; set; }
        public string AddInfo { get; set; }
        public short? LastUser { get; set; }
        public string Place { get; set; }
        public string Expendables { get; set; }

        public virtual staff User { get; set; }
        public virtual ICollection<JournalMachine> JournalMachines { get; set; }
        public virtual ICollection<License> Licenses { get; set; }
        public virtual ICollection<MachinesConnect> MachinesConnects { get; set; }
        public virtual ICollection<RemoveControl> RemoveControls { get; set; }
    }
}
