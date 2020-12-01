using System;
using System.Collections.Generic;

#nullable disable

namespace LTSMVC.Models
{
    public partial class AddressBook
    {
        public short IdNumTable { get; set; }
        public short? UserId { get; set; }
        public int? Number { get; set; }
        public string Post { get; set; }
        public string Place { get; set; }
        public string Sld { get; set; }

        public virtual staff User { get; set; }
    }
}
