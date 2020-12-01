using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class MessageText
    {
        public int MessagesIdMessage { get; set; }
        public string MessageText1 { get; set; }

        public virtual Message MessagesIdMessageNavigation { get; set; }
    }
}
