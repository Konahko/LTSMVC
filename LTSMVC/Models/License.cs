using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class License
    {
                                // А оно без клиента вообще надо?
        public int Id{ get; set; }
        public short StaffId { get; set; }
        public short MachinesId { get; set; }
        public string Pass { get; set; }
        public string Lisence { get; set; }

        public virtual Machine Machine { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
