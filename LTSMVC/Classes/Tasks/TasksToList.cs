using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Classes.Tasks
{
    public class TasksToList
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public short StaffId { get; set; }
        public string StaffName { get; set; }       //имя отправителя
        public short? Status { get; set; }
        public string TaskJob { get; set; }
        public string? NameSenderCommentName { get; set; }
        public short? NameSenderLastCommentId { get; set; }
        public string? LastComment { get; set; }
        public DateTime? DateSend { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime? DateClose { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
