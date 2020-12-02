using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class NetworkAddress
    {
        public short Id { get; set; }
        public short Network { get; set; } //Подсеть
        public short? IpAddress { get; set; } //Ip Адрес
        public short? MachinesId { get; set; }   //Id Закрепленной машины
        public string? Mac { get; set; } //MAC Адрес
        public string? AddressType { get; set; } 

        public virtual Machine Machine { get; set; }
    }
}
