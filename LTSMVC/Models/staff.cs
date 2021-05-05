using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Accounts = new HashSet<Account>();
            AddressBooks = new HashSet<AddressBook>();
            ExpendablesItems = new HashSet<ExpendablesItem>();
            Licenses = new HashSet<License>();
            Machines = new HashSet<Machine>();
            Tickets = new HashSet<Ticket>();
            StaffsTasks = new HashSet<StaffsTasks>();
            Messages = new HashSet<Message>();
            TasksComments = new HashSet<TasksComments>();
            Tasks = new HashSet<Task>();
            Events = new HashSet<Events>();
        }

        public short Id { get; set; }
        [DisplayName("ФИО")]
        public string StaffName { get; set; }
        [DisplayName("Подразделение")]
        public string StaffSub { get; set; }
        [DisplayName("Должность")]
        public string StaffPoss { get; set; }
        [DisplayName("Администратор")]
        public bool AdminU { get; set; }
        [DisplayName("Кабинет")]
        public string Place { get; set; }
        [DisplayName("Id Telegram")]
        public int? TgId { get; set; }
        [DisplayName("Имя пользователя Active Directory")]
        public string ADName { get; set; }

        public virtual ICollection<StaffsTasks> StaffsTasks { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<AddressBook> AddressBooks { get; set; }
        public virtual ICollection<ExpendablesItem> ExpendablesItems { get; set; }
        public virtual ICollection<License> Licenses { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<TasksComments> TasksComments { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Events> Events { get; set; }
    }
}
