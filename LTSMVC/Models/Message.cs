using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Message
    {
        public Message()
        {
            MessageFiles = new HashSet<MessageFile>();
        }

        public int IdMessage { get; set; }
        public short FromUser { get; set; }
        public short ToUser { get; set; }
        public sbyte IsOnlyFile { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<MessageFile> MessageFiles { get; set; }
    }
}
