using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Expendable
    {
        public Expendable()
        {
            ExpendablesItems = new HashSet<ExpendablesItem>();
        }

        public ushort Id { get; set; }   //Номер Типа расходника
        public string Name { get; set; }    //Имя Расходника
        public string Type { get; set; }    //Тип Расходника
        public int Amount { get; set; } // Количество Расходников

        public virtual ICollection<ExpendablesItem> ExpendablesItems { get; set; }
    }
}
