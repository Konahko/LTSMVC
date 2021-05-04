using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Classes.Tasks
{
    public class DialogPage
    {
        public int IdTask { get; set; }
        public string Name { get; set; }
        public short Status { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public DateTime DeadLine { get; set; }
        public int TaskSendlerInt { get; set; }
        public string TaskSendlerString { get; set; }
        public short UserId { get; set; }
    public List<DialogMessages> DialogMessages { get; set; }
    }

    public class DialogMessages
    {
        public short MessageSendlerid { get; set; }
        public string MessageSendlerString { get; set; }
        public DateTime MessageDate { get; set; }
        public string MessageText { get; set; }
    }
}
