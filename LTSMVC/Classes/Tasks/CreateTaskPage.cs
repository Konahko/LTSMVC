using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Classes.Tasks
{
    public class CreateTaskPage
    {
        public string Theme { get; set; }
        public string Task { get; set; }
        public string Comment { get; set; }
        public List<short> SelectedStaff { get; set; }
    }
}
