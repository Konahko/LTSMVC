using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class License
    {
        public int IdtLicense { get; set; }
        public short StaffStaffId { get; set; }
        public short MachinesIdMachines { get; set; }
        public string Pass { get; set; }
        public string Lisence { get; set; }

        public virtual Machine MachinesIdMachinesNavigation { get; set; }
        public virtual staff StaffStaff { get; set; }
    }
}
