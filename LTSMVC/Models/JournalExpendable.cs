using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class JournalExpendable
    {
        public int IdjournalExpendables { get; set; }
        public int ExpendablesItemsId { get; set; }
        public int? TrigerUser { get; set; }
        public string State { get; set; }
        public DateTime? Time { get; set; }

        public virtual ExpendablesItem ExpendablesItems { get; set; }
    }
}
