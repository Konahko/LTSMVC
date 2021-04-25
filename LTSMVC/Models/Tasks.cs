using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Task
    {
        public Task()
        {
            StaffsTasks = new HashSet<StaffsTasks>();
            TasksComments = new HashSet<TasksComments>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public short TaskSendler { get; set; } // id отправителя таски
        public string Job { get; set; }
        public short Status { get; set; } //active 0; complite 1; cancel 2
        public DateTime DateOpen { get; set; }
        public DateTime? DateClose { get; set; }
        public DateTime? Deadline { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<StaffsTasks> StaffsTasks { get; set; }
        public virtual ICollection<TasksComments> TasksComments { get; set; }
    }
}
