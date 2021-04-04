using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LTSMVC.Models
{
    public partial class Lts2Context : DbContext
    {
        public Lts2Context()
        {
        }

        public Lts2Context(DbContextOptions<Lts2Context> options)
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
        public virtual DbSet<NetworkAddress> NetworkAdresses { get; set; }
        public virtual DbSet<RemoveControl> RemoveControls { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Tasks> Tasks{ get; set; }
        public virtual DbSet<StaffsTasks> StaffsTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=lts_Udino;user=root;password=root;port=3307", ServerVersion.FromString("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StaffId, "fk_account_staff1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf32")
                    .HasCollation("utf32_bin");

                entity.Property(e => e.AddInfo)
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffId);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OutDate)
                    .HasColumnType("timestamp(6)");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf32")
                    .HasCollation("utf32_bin");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_staff1");
            });

            modelBuilder.Entity<AddressBook>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.StaffId, "fk_Num_table_staff1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.IpNumber);
                entity.Property(e => e.PhoneNumber);

                entity.Property(e => e.Place)
                    .HasColumnType("varchar(20)")
                    .IsRequired()
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Post)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Sld)
                    .HasColumnType("char(2)")
                    .IsRequired()
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffId)
                .IsRequired();

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.AddressBooks)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("fk_Num_table_staff1");
            });

            modelBuilder.Entity<Expendable>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");


                entity.Property(e => e.Id);

                entity.Property(e => e.Amount);

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
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ExpendablesId, "fk_Expendables_items_Expendables1_idx");

                entity.HasIndex(e => e.StaffId, "fk_Expendables_items_staff1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.ExpendablesId);

                entity.Property(e => e.StaffId);

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
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ExpendablesItemsId, "fk_journal_Expendables_Expendables_items1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.ExpendablesItemsId);

                entity.Property(e => e.State)
                    .HasColumnType("varchar(900)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnType("timestamp(6)");

                entity.Property(e => e.TriggerUser);

                entity.HasOne(d => d.ExpendablesItems)
                    .WithMany(p => p.JournalExpendables)
                    .HasForeignKey(d => d.ExpendablesItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_journal_Expendables_Expendables_items1");
            });

            modelBuilder.Entity<JournalMachine>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.MachinesId, "fk_Journal_Machines_Machines1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.MachinesId);

                entity.Property(e => e.State)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnType("timestamp(6)");

                entity.Property(e => e.TriggerUser);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.JournalMachines)
                    .HasForeignKey(d => d.MachinesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Journal_Machines_Machines1");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.MachinesId, "fk_License_Machines1_idx");

                entity.HasIndex(e => e.StaffId, "fk_License_staff1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.Lisence)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MachinesId);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnType("char(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffId);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Licenses)
                    .HasForeignKey(d => d.MachinesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_License_Machines1");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Licenses)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_License_staff1");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Id, "fk_Machines_staff_index");

                entity.HasIndex(e => e.Id, "id_Machines_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.Property(e => e.AddInfo)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Character)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Expendables)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InvNumber)
                    .IsRequired()
                    .HasColumnType("char(13)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastUser);

                entity.Property(e => e.Mod)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
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
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffId);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_staff");
            });

            modelBuilder.Entity<MachinesConnect>(entity =>
            {
                entity.HasKey(e => e.id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.MachinesId, "fk_Machines_connect_Machines1_idx");

                entity.Property(e => e.id);

                entity.Property(e => e.IsAdmin);

                entity.Property(e => e.Login)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MachinesId);

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

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.StaffId, "fk_chat_staff1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.DateClose)
                    .HasColumnType("timestamp(6)");

                entity.Property(e => e.DateOpen)
                    .HasColumnType("timestamp(6)")
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");
                entity.Property(e => e.WorkerId);

                entity.Property(e => e.TicketProblem)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffId);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_chat_staff1");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.TicketId, "fk_ticketid_ticket_idx");

                entity.Property(e => e.TicketId);

                entity.Property(e => e.Id);

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp(6)");

                entity.Property(e => e.FromUser);

                entity.Property(e => e.IsOnlyFile);

                entity.HasOne(d => d.Ticket)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticketid_ticket");
            });

            modelBuilder.Entity<MessageFile>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.MessageId, "fk_Message_File_Messages1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasColumnType("varchar(7)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MessageId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageFiles)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Message_File_Messages1");
            });

            modelBuilder.Entity<MessageText>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.MessageId, "fk_Message_text_Messages1_idx");

                entity.Property(e => e.Text)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Id);

                entity.HasOne(d => d.Message)
                    .WithMany (p => p.MessageText)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Message_text_Messages1");
            });

            modelBuilder.Entity<NetworkAddress>(entity =>
            {

                entity.HasKey(e => e.Id)
                .HasName("PRIMARY");

                entity.HasIndex(e => e.MachinesId, "fk_Network_address_Machines1_idx");

                entity.Property(e => e.Id)
                .IsRequired();

                entity.HasIndex(e => e.Id, "id_NetworkAddress_UNIQUE")
                .IsUnique();

                entity.Property(e => e.AddressType)
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IpAddress);

                entity.Property(e => e.Mac)
                    .IsRequired()
                    .HasColumnType("varchar(17)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MachinesId);

                entity.HasOne(d => d.Machine)
                    .WithMany()
                    .HasForeignKey(d => d.MachinesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Network_address_Machines1");
            });

            modelBuilder.Entity<RemoveControl>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.MachinesId, "fk_connected_machines_Machines1_idx");

                entity.Property(e => e.Id);

                entity.Property(e => e.AmmyAdmin)
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AmmyAdminPassword)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnyDesk)
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnyDeskPassword)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MachinesId);

                entity.Property(e => e.Rdp)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TeamViewer)
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TeamViewerPassword)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Machines)
                    .WithMany(p => p.RemoveControls)
                    .HasForeignKey(d => d.MachinesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_connected_machines_Machines1");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasIndex(e => e.Id, "Staff_id_3")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.Property(e => e.AdminU);

                entity.Property(e => e.Place)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf32")
                    .HasCollation("utf32_bin");

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffPoss)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StaffSub)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TgId)
                .HasDefaultValueSql("'0'");

                entity.Property(e => e.ADName)
                .IsRequired()
                .HasColumnType("varchar(55)")
                .HasCharSet("utf32")
                .HasCollation("utf32_bin");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasIndex(e => e.Id, "Tasks_id_3")
                    .IsUnique();
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");
                entity.Property(e => e.Id);

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
                entity.Property(e => e.Task)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
                entity.Property(e => e.Status)
                    .HasDefaultValue(0);
                entity.Property(e => e.DateOpen)
                    .HasColumnType("timestamp(6)");
                entity.Property(e => e.DateClose)
                    .HasColumnType("timestamp(6)");
                entity.Property(e => e.Deadline)
                    .HasColumnType("timestamp(6)");
            });

            modelBuilder.Entity<StaffsTasks>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .IsUnique();
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");
                entity.Property(e => e.Id);
                entity.Property(e => e.StaffId);
                entity.HasIndex(e => e.StaffId);
                entity.Property(e => e.Task);
                entity.HasIndex(e => e.Task);
                entity.Property(e => e.Status)
                    .HasDefaultValue(0);
                entity.HasOne(d => d.Tasks)
                    .WithMany(e => e.StaffsTasks)
                    .HasForeignKey(d => d.Task)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fr_StaffsTasks_Task1");
                entity.HasOne(d => d.Staff)
                    .WithMany(e => e.StaffsTasks)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fr_StaffsTasks_Staff1");


            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
