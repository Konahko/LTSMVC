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

        public int Id { get; set; } //Номер сообщения
        public short FromUser { get; set; } //От кого
        public short ToUser { get; set; }   //Кому
        public sbyte IsOnlyFile { get; set; }   //Пометка о том, файл ли это
        public DateTime Date { get; set; }  //Время сообщения

        public virtual ICollection<MessageFile> MessageFiles { get; set; }
    }
}
