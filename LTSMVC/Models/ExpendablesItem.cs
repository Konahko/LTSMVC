using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class ExpendablesItem
    {
        public ExpendablesItem()
        {
            JournalExpendables = new HashSet<JournalExpendable>();
        }

        public int IdExpendablesItems { get; set; }
        public ushort ExpendablesId { get; set; }
        public string Status { get; set; }
        public short StaffId { get; set; }

        public virtual Expendable Expendables { get; set; }
        public virtual staff Staff { get; set; }
        public virtual ICollection<JournalExpendable> JournalExpendables { get; set; }
    }
}
