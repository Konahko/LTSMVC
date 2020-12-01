using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class MachinesConnect
    {
        public short IdMachinesConnect { get; set; }
        public short MachinesId { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public sbyte? IsAdmin { get; set; }

        public virtual Machine Machines { get; set; }
    }
}
