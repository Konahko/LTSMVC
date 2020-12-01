using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class MessageFile
    {
        public int IdMessageFile { get; set; }
        public int MessagesIdMessage { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }

        public virtual Message MessagesIdMessageNavigation { get; set; }
    }
}
