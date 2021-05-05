using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTSMVC.Models;


namespace LTSMVC.Classes.Events
{
    public class EventsPage
    {
        public List<LTSMVC.Models.Events> NewEvents{get; set;}
        public List<LTSMVC.Models.Events> OldEvents { get; set; }
    }
}
