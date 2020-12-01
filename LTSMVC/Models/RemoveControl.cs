using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class RemoveControl
    {
        public short IdconnectedMachines { get; set; }
        public short MachinesId { get; set; }
        public string AnyDesk { get; set; }
        public string AnyDeskPass { get; set; }
        public string TeamViewer { get; set; }
        public string TeamViewerPass { get; set; }
        public string AmmyAdmin { get; set; }
        public string AmmyAdminPass { get; set; }
        public string Rdp { get; set; }

        public virtual Machine Machines { get; set; }
    }
}
