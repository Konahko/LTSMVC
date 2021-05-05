using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Models
{
    public class Events
    {
        public int Id { get; set; }
        public short EventGenerator { get; set; }
        public string Theme { get; set; }
        public string Event { get; set; }
        public DateTime DateTimeOfEvent { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
