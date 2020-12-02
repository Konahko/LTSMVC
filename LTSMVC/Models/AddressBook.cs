using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class AddressBook
    {
        public short Id { get; set; }   //Id
        public short? StaffId { get; set; }  //Id Сотрудника из Staff
        public int? IpNumber { get; set; }   //IP Телефон
        public int? PhoneNumber { get; set; } // Мобильный телефон
        public string Post { get; set; }    // Почта
        public string Place { get; set; }   // Местоположение
        public string Sld { get; set; } //Номер СЛД

        public virtual Staff Staff { get; set; }
    }
}
