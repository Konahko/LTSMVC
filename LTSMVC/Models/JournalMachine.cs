using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class JournalMachine
    {
        public int Id { get; set; } // Номер записи в журнале
        public short MachinesId { get; set; } //Номер Машины
        public int? TrigerUser { get; set; }    // Юзер справоцировавшего запись Придумать как обойти    
        public string State { get; set; }   // Состояние
        public DateTime? Time { get; set; } //Время записи

        public virtual Machine Machine { get; set; }
    }
}
