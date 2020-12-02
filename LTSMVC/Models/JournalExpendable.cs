using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class JournalExpendable
    {
        public int Id { get; set; }   //Id Записи в журнал 
        public int ExpendablesItemsId { get; set; } //Id Конкретного Расходника
        public int? TrigerUser { get; set; }    //Юзер справоцировавшего запись Придумать как обойти    
        public string State { get; set; }   //Состояние
        public DateTime? Time { get; set; } //Время срабатывания

        public virtual ExpendablesItem ExpendablesItems { get; set; }
    }
}
