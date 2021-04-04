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
            MessageText = new HashSet<MessageText>();
        }

        public int Id { get; set; } //Номер сообщения

        public int TicketId { get; set; } // Номер Тикета
        public short FromUser { get; set; } //От кого
        public sbyte IsOnlyFile { get; set; }   //Пометка о том, файл ли это
        public DateTime Date { get; set; }  //Время сообщения

        public virtual Ticket Ticket { get; set; }
        public virtual ICollection<MessageFile> MessageFiles { get; set; }
        public virtual ICollection<MessageText> MessageText { get; set; }
    }
}
