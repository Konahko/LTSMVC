using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTSMVC.Models;

namespace LTSMVC.Classes.Home
{
    public class HomeIndex
    {
        public List<Ticket> Tickets;
        public Ticket TicketObject = new Ticket();
        public int CountNewTickets;
        public int CountActiveTikets;

        public List<StaffsTasks> StaffsTasks;
        public StaffsTasks TasksObject = new StaffsTasks();
        public int CountActiveTasks;
    }
}
