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

        public int Id { get; set; } // Номер конкретного расходника
        public ushort ExpendablesId { get; set; } // Id Расходника
        public string Status { get; set; }  //Id Расходника
        public short StaffId { get; set; }  //Id Сотрудника кому передано

        public virtual Expendable Expendables { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<JournalExpendable> JournalExpendables { get; set; }
    }
}
