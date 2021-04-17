using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTSMVC.Models;

namespace LTSMVC.Classes.Tickets
{
    public class DialogMessages
    {
        public int Id;  
        public short FromUser;
        public string FromUserName;
        public sbyte IsOnlyFile;
        public DateTime Date;
        public string MessageText;

        public List<MessageFile> messageFiles;
    }
}
