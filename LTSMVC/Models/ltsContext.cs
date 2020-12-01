using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LTSMVC.Models
{
    public partial class ltsContext : DbContext
    {
        public ltsContext()
        {
        }

        public ltsContext(DbContextOptions<ltsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AddressBook> AddressBooks { get; set; }
        public virtual DbSet<Expendable> Expendables { get; set; }
        public virtual DbSet<ExpendablesItem> ExpendablesItems { get; set; }
        public virtual DbSet<JournalExpendable> JournalExpendables { get; set; }
        public virtual DbSet<JournalMachine> JournalMachines { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<MachinesConnect> MachinesConnects { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageFile> MessageFiles { get; set; }
        public virtual DbSet<MessageText> MessageTexts { get; set; }
        public virtual DbSet<NetworkAdress> NetworkAdresses { get; set; }
        public virtual DbSet<RemoveControl> RemoveControls { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketFile> TicketFiles { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=lts;user=root;password=root;port=3307", Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount)
                    .HasName("PRIMARY");

                entity.ToTable("account");

                entity.HasIndex(e => e.IdAccount, "account_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdStaff, "fk_account_staff1_idx");

                entity.Property(e => e.IdAccount).HasColumnName("idAccount");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasColumnName("account_type")
                    .HasCharSet("utf32")
                    .HasCollation("utf32_bin");

                entity.Property(e => e.AddInfo)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("add_info")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdStaff).HasColumnName("idStaff");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasColumnName("login")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OutDate)
                    .HasColumnType("timestamp(6)")
                    .HasColumnName("out_date");

                entity.Property(e => e.Pass)
                    .HasColumnType("varchar(16)")
                    .HasColumnName("pass")
                    .HasCharSet("utf32")
                    .HasCollation("utf32_bin");

                entity.HasOne(d => d.IdStaffNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.IdStaff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_staff1");
            });

            modelBuilder.Entity<AddressBook>(entity =>
            {
                entity.HasKey(e => e.IdNumTable)
                    .HasName("PRIMARY");

                entity.ToTable("address_book");

                entity.HasIndex(e => e.UserId, "fk_Num_table_staff1_idx");

                entity.Property(e => e.IdNumTable).HasColumnName("idNum_table");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Place)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("place")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Post)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasColumnName("post")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Sld)
                    .HasColumnType("char(2)")
                    .HasColumnName("SLD")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AddressBooks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_Num_table_staff1");
            });

            modelBuilder.Entity<Expendable>(entity =>
            {
                entity.HasKey(e => e.IdExpendables)
                    .HasName("PRIMARY");

                entity.ToTable("expendables");

                entity.Property(e => e.IdExpendables).HasColumnName("idExpendables");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<ExpendablesItem>(entity =>
            {
                entity.HasKey(e => e.IdExpendablesItems)
                    .HasName("PRIMARY");

                entity.ToTable("expendables_items");

                entity.HasIndex(e => e.ExpendablesId, "fk_Expendables_items_Expendables1_idx");

                entity.HasIndex(e => e.StaffId, "fk_Expendables_items_staff1_idx");

                entity.Property(e => e.IdExpendablesItems).HasColumnName("idExpendables_items");

                entity.Property(e => e.ExpendablesId).HasColumnName("Expendables_id");

                entity.Property(e => e.StaffId).HasColumnName("Staff_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(35)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Expendables)
                    .WithMany(p => p.ExpendablesItems)
                    .HasForeignKey(d => d.ExpendablesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Expendables_items_Expendables1");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.ExpendablesItems)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Expendables_items_staff1");
            });

            modelBuilder.Entity<JournalExpendable>(entity =>
            {
                entity.HasKey(e => e.IdjournalExpendables)
                    .HasName("PRIMARY");

                entity.ToTable("journal_expendables");

                entity.HasIndex(e => e.ExpendablesItemsId, "fk_journal_Expendables_Expendables_items1_idx");

                entity.Property(e => e.IdjournalExpendables).HasColumnName("idjournal_Expendables");

                entity.Property(e => e.ExpendablesItemsId).HasColumnName("Expendables_items_id");

                entity.Property(e => e.State)
                    .HasColumnType("varchar(900)")
                    .HasColumnName("state")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnType("timestamp(6)")
                    .HasColumnName("time");

                entity.Property(e => e.TrigerUser).HasColumnName("Triger_user");

                entity.HasOne(d => d.ExpendablesItems)
                    .WithMany(p => p.JournalExpendables)
                    .HasForeignKey(d => d.ExpendablesItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_journal_Expendables_Expendables_items1");
            });

            modelBuilder.Entity<JournalMachine>(entity =>
            {
                entity.HasKey(e => e.IdJournalMachines)
                    .HasName("PRIMARY");

                entity.ToTable("journal_machines");

                entity.HasIndex(e => e.MachinesIdMachines, "fk_Journal_Machines_Machines1_idx");

                entity.Property(e => e.IdJournalMachines).HasColumnName("idJournal_Machines");

                entity.Property(e => e.MachinesIdMachines).HasColumnName("Machines_id_Machines");

                entity.Property(e => e.State)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Time).HasColumnType("timestamp(6)");

                entity.Property(e => e.TrigerUser).HasColumnName("Triger_user");

                entity.HasOne(d => d.MachinesIdMachinesNavigation)
                    .WithMany(p => p.JournalMachines)
                    .HasForeignKey(d => d.MachinesIdMachines)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Journal_Machines_Machines1");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.HasKey(e => e.IdtLicense)
                    .HasName("PRIMARY");

                entity.ToTable("license");

                entity.HasIndex(e => e.MachinesIdMachines, "fk_License_Machines1_idx");

                entity.HasIndex(e => e.StaffStaffId, "fk_License_staff1_idx");

                entity.Property(e => e.IdtLicense).HasColumnName("idtLicense");

                entity.Property(e => e.Lisence)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MachinesIdMachines).HasColumnName("Machines_id_Machines");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnType("char(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffStaffId).HasColumnName("staff_Staff_id");

                entity.HasOne(d => d.MachinesIdMachinesNavigation)
                    .WithMany(p => p.Licenses)
                    .HasForeignKey(d => d.MachinesIdMachines)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_License_Machines1");

                entity.HasOne(d => d.StaffStaff)
                    .WithMany(p => p.Licenses)
                    .HasForeignKey(d => d.StaffStaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_License_staff1");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(e => e.IdMachines)
                    .HasName("PRIMARY");

                entity.ToTable("machines");

                entity.HasIndex(e => e.UserId, "fk_Machines_staff");

                entity.HasIndex(e => e.IdMachines, "id_Machines_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdMachines).HasColumnName("id_Machines");

                entity.Property(e => e.AddInfo)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("add_info")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Charecter)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("charecter")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Expendables)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InvNumber)
                    .IsRequired()
                    .HasColumnType("char(13)")
                    .HasColumnName("inv_number")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastUser).HasColumnName("last_user");

                entity.Property(e => e.Mod)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("mod")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Place)
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("'Склад'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasColumnName("type")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_staff");
            });

            modelBuilder.Entity<MachinesConnect>(entity =>
            {
                entity.HasKey(e => e.IdMachinesConnect)
                    .HasName("PRIMARY");

                entity.ToTable("machines_connect");

                entity.HasIndex(e => e.MachinesId, "fk_Machines_connect_Machines1_idx");

                entity.Property(e => e.IdMachinesConnect).HasColumnName("idMachines_connect");

                entity.Property(e => e.IsAdmin).HasColumnName("Is_Admin");

                entity.Property(e => e.Login)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MachinesId).HasColumnName("Machines_id");

                entity.Property(e => e.Pass)
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Machines)
                    .WithMany(p => p.MachinesConnects)
                    .HasForeignKey(d => d.MachinesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_connect_Machines1");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.IdMessage)
                    .HasName("PRIMARY");

                entity.ToTable("messages");

                entity.Property(e => e.IdMessage).HasColumnName("idMessage");

                entity.Property(e => e.Date).HasColumnType("timestamp(6)");

                entity.Property(e => e.FromUser).HasColumnName("From_user");

                entity.Property(e => e.IsOnlyFile).HasColumnName("is_only_file");

                entity.Property(e => e.ToUser).HasColumnName("To_user");
            });

            modelBuilder.Entity<MessageFile>(entity =>
            {
                entity.HasKey(e => e.IdMessageFile)
                    .HasName("PRIMARY");

                entity.ToTable("message_file");

                entity.HasIndex(e => e.MessagesIdMessage, "fk_Message_File_Messages1_idx");

                entity.Property(e => e.IdMessageFile).HasColumnName("idMessage_file");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasColumnType("varchar(7)")
                    .HasColumnName("Data_type")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MessagesIdMessage).HasColumnName("Messages_idMessage");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.MessagesIdMessageNavigation)
                    .WithMany(p => p.MessageFiles)
                    .HasForeignKey(d => d.MessagesIdMessage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Message_File_Messages1");
            });

            modelBuilder.Entity<MessageText>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("message_text");

                entity.HasIndex(e => e.MessagesIdMessage, "fk_Message_text_Messages1_idx");

                entity.Property(e => e.MessageText1)
                    .HasColumnType("varchar(1000)")
                    .HasColumnName("Message_text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MessagesIdMessage).HasColumnName("Messages_idMessage");

                entity.HasOne(d => d.MessagesIdMessageNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MessagesIdMessage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Message_text_Messages1");
            });

            modelBuilder.Entity<NetworkAdress>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("network_adress");

                entity.HasIndex(e => e.MachinesIdMachines, "fk_Network_adress_Machines1_idx");

                entity.Property(e => e.AddressType)
                    .HasColumnType("char(1)")
                    .HasColumnName("Address_type")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IpAdress).HasColumnName("Ip_Adress");

                entity.Property(e => e.Mac)
                    .IsRequired()
                    .HasColumnType("varchar(17)")
                    .HasColumnName("MAC")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MachinesIdMachines).HasColumnName("Machines_id_Machines");

                entity.HasOne(d => d.MachinesIdMachinesNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MachinesIdMachines)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Network_adress_Machines1");
            });

            modelBuilder.Entity<RemoveControl>(entity =>
            {
                entity.HasKey(e => e.IdconnectedMachines)
                    .HasName("PRIMARY");

                entity.ToTable("remove_control");

                entity.HasIndex(e => e.MachinesId, "fk_connected_machines_Machines1_idx");

                entity.Property(e => e.IdconnectedMachines).HasColumnName("idconnected_machines");

                entity.Property(e => e.AmmyAdmin)
                    .HasColumnType("varchar(10)")
                    .HasColumnName("Ammy_Admin")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AmmyAdminPass)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("Ammy_Admin_pass")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnyDesk)
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnyDeskPass)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("AnyDesk_pass")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MachinesId).HasColumnName("Machines_id");

                entity.Property(e => e.Rdp)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("rdp")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TeamViewer)
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TeamViewerPass)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("TeamViewer_pass")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Machines)
                    .WithMany(p => p.RemoveControls)
                    .HasForeignKey(d => d.MachinesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_connected_machines_Machines1");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("PRIMARY");

                entity.ToTable("ticket");

                entity.HasIndex(e => e.UserId, "fk_chat_staff1_idx");

                entity.Property(e => e.IdTicket).HasColumnName("id_ticket");

                entity.Property(e => e.DateClose)
                    .HasColumnType("timestamp(6)")
                    .HasColumnName("Date_close");

                entity.Property(e => e.DateOpen)
                    .HasColumnType("timestamp(6)")
                    .HasColumnName("Date_open");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.TicketProblem)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_chat_staff1");
            });

            modelBuilder.Entity<TicketFile>(entity =>
            {
                entity.HasKey(e => e.IdMessageFile)
                    .HasName("PRIMARY");

                entity.ToTable("ticket_file");

                entity.HasIndex(e => e.TicketIdTicket, "fk_ticket_file_Ticket1_idx");

                entity.Property(e => e.IdMessageFile).HasColumnName("id_message_file");

                entity.Property(e => e.DataType)
                    .HasColumnType("varchar(8)")
                    .HasColumnName("data_type")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TicketIdTicket).HasColumnName("Ticket_id_ticket");

                entity.HasOne(d => d.TicketIdTicketNavigation)
                    .WithMany(p => p.TicketFiles)
                    .HasForeignKey(d => d.TicketIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ticket_file_Ticket1");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasIndex(e => e.StaffId, "Staff_id_3")
                    .IsUnique();

                entity.Property(e => e.StaffId).HasColumnName("Staff_id");

                entity.Property(e => e.AdminU).HasColumnName("admin_u");

                entity.Property(e => e.Place)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("place")
                    .HasCharSet("utf32")
                    .HasCollation("utf32_bin");

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasColumnType("varchar(40)")
                    .HasColumnName("Staff_Name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffPoss)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasColumnName("Staff_Poss")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffSub)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasColumnName("Staff_Sub")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TgId).HasColumnName("tg_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
