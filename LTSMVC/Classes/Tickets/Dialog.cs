using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTSMVC.Classes.Tickets;

namespace LTSMVC.Classes.Tickets
{
    public class Dialog
    {
        public int Id;
        public string Subj;
        public DateTime DateOpen;
        public string User;
        public short UserId;
        public string Partner;
        public short PartnerId;
        public bool IsOpen;

        public List<DialogMessages> DialogMessages;

    }
}
