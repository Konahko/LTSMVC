using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Tasks
    {
        public Tasks()
        {
            StaffsTasks = new HashSet<StaffsTasks>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Task { get; set; }
        public short Status { get; set; } //active 0; complite 1; cancel 2
        public DateTime DateOpen { get; set; }
        public DateTime? DateClose { get; set; }
        public DateTime? Deadline { get; set; }
        public virtual ICollection<StaffsTasks> StaffsTasks { get; set; }
    }
}
