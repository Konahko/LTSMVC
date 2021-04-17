using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class MessageFile
    {
        public int Id { get; set; } //Id Файла
        public int MessageId { get; set; }  //Id Закрепленного сообщения
        public string DataType { get; set; } //Тип данных
        public string Name { get; set; } //Имя

        public virtual Message Messages { get; set; }
    }
}
