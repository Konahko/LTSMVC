using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LTSMVC.Models
{
    public class ExpendablesItem
    {
        public int Id { get; set; } // Номер конкретного расходника
        public ushort ExpendablesId { get; set; } // Id Расходника
        public string Status { get; set; }  //Id Расходника
        public short StaffId { get; set; }  //Id Сотрудника кому передано

        public virtual Expendable Expendables { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<JournalExpendable> JournalExpendables { get; set; }

        public ExpendablesItem()
        {
            JournalExpendables = new HashSet<JournalExpendable>();
        }

        public string GetQrText(DateTime dateTime)
        {
            //генерация MD5
            var md5 = new MD5CryptoServiceProvider();
            var bytes = Encoding.Unicode.GetBytes($"Нормальные диски поставь, э [{ToString()}]");
            
            var checksum = BitConverter.ToString(md5.ComputeHash(bytes));
            return $"{ToString()}\n" +
                   $"PrintDate={dateTime.ToShortDateString()}\n" +
                   $"Hash={checksum}";
        }
        

        public override string ToString()
        {
            return $"Id={Id}\n" +
                   $"ExpendaplesId={ExpendablesId}\n" +
                   $"ExpendableName={Expendables.Name}\n" +
                   $"Type={Expendables.Type}";
        }
    }
}
