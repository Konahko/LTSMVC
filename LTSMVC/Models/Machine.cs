using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Machine
    {
        public Machine()
        {
            JournalMachines = new HashSet<JournalMachine>();
            Licenses = new HashSet<License>();
            MachinesConnects = new HashSet<MachinesConnect>();
            RemoveControls = new HashSet<RemoveControl>();
        }

        public short Id{ get; set; }    // Id Машины
        public short StaffId { get; set; }   // Id Юсера
        public string Type { get; set; }    //Тип Машины
        public string Name { get; set; }    //Наименование
        public string InvNumber { get; set; }   //Инвернтарный номер
        public string Status { get; set; }  //состояние
        public string Character { get; set; }   //Характеристики
        public string Mod { get; set; } //Модификации
        public string AddInfo { get; set; } //Дополнительная информация
        public short? LastUser { get; set; }    //Предыдущий юзер
        public string Place { get; set; }   //Местоположение
        public string Expendables { get; set; } //Расходные материалы

        public virtual Staff Staff { get; set; }
        public virtual ICollection<JournalMachine> JournalMachines { get; set; }
        public virtual ICollection<License> Licenses { get; set; }
        public virtual ICollection<MachinesConnect> MachinesConnects { get; set; }
        public virtual ICollection<RemoveControl> RemoveControls { get; set; }
    }
}
