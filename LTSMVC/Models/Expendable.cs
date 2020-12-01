using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Expendable
    {
        public Expendable()
        {
            ExpendablesItems = new HashSet<ExpendablesItem>();
        }

        public ushort IdExpendables { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }

        public virtual ICollection<ExpendablesItem> ExpendablesItems { get; set; }
    }
}
