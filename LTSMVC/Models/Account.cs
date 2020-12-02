using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Account
    {
        public short Id { get; set; } //Id 
        public short StaffId { get; set; } // Id Сотрудника из Staff
        public string AccountType { get; set; } //Тип аккаунта
        [DisplayName("Логин")]
        public string Login { get; set; }   // Логин
        public string Pass { get; set; }    // Пароль
        public DateTime? OutDate { get; set; }  // Срок действия пароля
        public string AddInfo { get; set; } // Доп инфа

        public virtual Staff Staff { get; set; }
    }
}
