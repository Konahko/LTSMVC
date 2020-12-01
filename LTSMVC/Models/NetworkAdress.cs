using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class NetworkAdress
    {
        public short Network { get; set; }
        public short? IpAdress { get; set; }
        public short MachinesIdMachines { get; set; }
        public string Mac { get; set; }
        public string AddressType { get; set; }

        public virtual Machine MachinesIdMachinesNavigation { get; set; }
    }
}
