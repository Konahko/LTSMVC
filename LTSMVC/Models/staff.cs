using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class staff
    {
        public staff()
        {
            Accounts = new HashSet<Account>();
            AddressBooks = new HashSet<AddressBook>();
            ExpendablesItems = new HashSet<ExpendablesItem>();
            Licenses = new HashSet<License>();
            Machines = new HashSet<Machine>();
            Tickets = new HashSet<Ticket>();
        }

        public short StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffSub { get; set; }
        public string StaffPoss { get; set; }
        public bool AdminU { get; set; }
        public string Place { get; set; }
        public int TgId { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<AddressBook> AddressBooks { get; set; }
        public virtual ICollection<ExpendablesItem> ExpendablesItems { get; set; }
        public virtual ICollection<License> Licenses { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
