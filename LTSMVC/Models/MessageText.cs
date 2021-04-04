using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class MessageText
    {
        public int Id { get; set; } //Id
        public int MessageId { get; set; } //MessageId
        public string Text { get; set; } //Текст Сообщения

        public virtual Message Message { get; set; }
    }
}
