using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Account
    {
        public short IdAccount { get; set; }
        public short IdStaff { get; set; }
        public string AccountType { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public DateTime? OutDate { get; set; }
        public string AddInfo { get; set; }

        public virtual staff IdStaffNavigation { get; set; }
    }
}
