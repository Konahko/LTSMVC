using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class TasksComments
    {
        public int Id { get; set; } //Номер сообщения
        public int Task { get; set; } // Номер Тикета
        public short FromUser { get; set; } //От кого
        public DateTime Date { get; set; }  //Время сообщения
        public string CommentText { get; set; } // Текст сообщения

        public virtual Task Tasks { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
