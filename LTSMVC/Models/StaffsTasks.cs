using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace LTSMVC.Models
{
    public partial class StaffsTasks
    {
        public short Id { get; set; } //Id 
        public short StaffId { get; set; } // Id Сотрудника из Staff
        public int Task { get; set; }
        public short Status { get; set; } //active 0; complite 1; cancel 2
        public virtual Staff Staff { get; set; }
        public virtual Task Tasks { get; set; }
    }
}
