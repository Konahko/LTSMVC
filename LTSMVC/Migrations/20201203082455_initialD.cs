using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LTSMVC.Migrations
{
    public partial class initialD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expendables",
                columns: table => new
                {
                    Id = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Type = table.Column<string>(type: "varchar(45)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FromUser = table.Column<short>(type: "smallint", nullable: false),
                    ToUser = table.Column<short>(type: "smallint", nullable: false),
                    IsOnlyFile = table.Column<sbyte>(type: "tinyint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffName = table.Column<string>(type: "varchar(40)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    StaffSub = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    StaffPoss = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    AdminU = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Place = table.Column<string>(type: "varchar(45)", nullable: true, collation: "utf32_bin")
                        .Annotation("MySql:CharSet", "utf32"),
                    TgId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    DataType = table.Column<string>(type: "varchar(7)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Message_File_Messages1",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "varchar(1000)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_Message_text_Messages1",
                        column: x => x.Id,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<short>(type: "smallint", nullable: false),
                    AccountType = table.Column<string>(type: "varchar(10)", nullable: false, collation: "utf32_bin")
                        .Annotation("MySql:CharSet", "utf32"),
                    Login = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Password = table.Column<string>(type: "varchar(16)", nullable: true, collation: "utf32_bin")
                        .Annotation("MySql:CharSet", "utf32"),
                    OutDate = table.Column<DateTime>(type: "timestamp(6)", nullable: true),
                    AddInfo = table.Column<string>(type: "varchar(30)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_account_staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AddressBooks",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<short>(type: "smallint", nullable: false),
                    IpNumber = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true),
                    Post = table.Column<string>(type: "varchar(60)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Place = table.Column<string>(type: "varchar(20)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Sld = table.Column<string>(type: "char(2)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Num_table_staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpendablesItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExpendablesId = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    Status = table.Column<string>(type: "varchar(35)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    StaffId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Expendables_items_Expendables1",
                        column: x => x.ExpendablesId,
                        principalTable: "Expendables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Expendables_items_staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<short>(type: "smallint", nullable: false),
                    Type = table.Column<string>(type: "varchar(10)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Name = table.Column<string>(type: "varchar(60)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    InvNumber = table.Column<string>(type: "char(13)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Status = table.Column<string>(type: "varchar(10)", nullable: true, defaultValueSql: "'Склад'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Character = table.Column<string>(type: "varchar(100)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Mod = table.Column<string>(type: "varchar(50)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    AddInfo = table.Column<string>(type: "varchar(45)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    LastUser = table.Column<short>(type: "smallint", nullable: true),
                    Place = table.Column<string>(type: "varchar(30)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Expendables = table.Column<string>(type: "varchar(300)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Machines_staff",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<short>(type: "smallint", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    TicketProblem = table.Column<string>(type: "varchar(500)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    DateOpen = table.Column<DateTime>(type: "timestamp(6)", nullable: false),
                    DateClose = table.Column<DateTime>(type: "timestamp(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_chat_staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalExpendables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExpendablesItemsId = table.Column<int>(type: "int", nullable: false),
                    TriggerUser = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<string>(type: "varchar(900)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Time = table.Column<DateTime>(type: "timestamp(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_journal_Expendables_Expendables_items1",
                        column: x => x.ExpendablesItemsId,
                        principalTable: "ExpendablesItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalMachines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MachinesId = table.Column<short>(type: "smallint", nullable: false),
                    TriggerUser = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<string>(type: "varchar(2000)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Time = table.Column<DateTime>(type: "timestamp(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Journal_Machines_Machines1",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<short>(type: "smallint", nullable: false),
                    MachinesId = table.Column<short>(type: "smallint", nullable: false),
                    Pass = table.Column<string>(type: "char(4)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Lisence = table.Column<string>(type: "varchar(16)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_License_Machines1",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_License_staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MachinesConnects",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MachinesId = table.Column<short>(type: "smallint", nullable: false),
                    Login = table.Column<string>(type: "varchar(15)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Pass = table.Column<string>(type: "varchar(16)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    IsAdmin = table.Column<sbyte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_Machines_connect_Machines1",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NetworkAdresses",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    Network = table.Column<short>(type: "smallint", nullable: false),
                    IpAddress = table.Column<short>(type: "smallint", nullable: true),
                    MachinesId = table.Column<short>(type: "smallint", nullable: true),
                    Mac = table.Column<string>(type: "varchar(17)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    AddressType = table.Column<string>(type: "char(1)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_Network_address_Machines1",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RemoveControls",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MachinesId = table.Column<short>(type: "smallint", nullable: false),
                    AnyDesk = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    AnyDeskPassword = table.Column<string>(type: "varchar(20)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    TeamViewer = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    TeamViewerPassword = table.Column<string>(type: "varchar(20)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    AmmyAdmin = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    AmmyAdminPassword = table.Column<string>(type: "varchar(20)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Rdp = table.Column<string>(type: "varchar(45)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_connected_machines_Machines1",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    DataType = table.Column<string>(type: "varchar(8)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_ticket_file_Ticket1",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_account_staff1_idx",
                table: "Accounts",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "Accounts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_Num_table_staff1_idx",
                table: "AddressBooks",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "fk_Expendables_items_Expendables1_idx",
                table: "ExpendablesItems",
                column: "ExpendablesId");

            migrationBuilder.CreateIndex(
                name: "fk_Expendables_items_staff1_idx",
                table: "ExpendablesItems",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "fk_journal_Expendables_Expendables_items1_idx",
                table: "JournalExpendables",
                column: "ExpendablesItemsId");

            migrationBuilder.CreateIndex(
                name: "fk_Journal_Machines_Machines1_idx",
                table: "JournalMachines",
                column: "MachinesId");

            migrationBuilder.CreateIndex(
                name: "fk_License_Machines1_idx",
                table: "Licenses",
                column: "MachinesId");

            migrationBuilder.CreateIndex(
                name: "fk_License_staff1_idx",
                table: "Licenses",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "fk_Machines_staff_index",
                table: "Machines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "id_Machines_UNIQUE",
                table: "Machines",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_StaffId",
                table: "Machines",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "fk_Machines_connect_Machines1_idx",
                table: "MachinesConnects",
                column: "MachinesId");

            migrationBuilder.CreateIndex(
                name: "fk_Message_File_Messages1_idx",
                table: "MessageFiles",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "fk_Message_text_Messages1_idx",
                table: "MessageTexts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "fk_Network_address_Machines1_idx",
                table: "NetworkAdresses",
                column: "MachinesId");

            migrationBuilder.CreateIndex(
                name: "fk_connected_machines_Machines1_idx",
                table: "RemoveControls",
                column: "MachinesId");

            migrationBuilder.CreateIndex(
                name: "Staff_id_3",
                table: "Staff",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_ticket_file_Ticket1_idx",
                table: "TicketFiles",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "fk_chat_staff1_idx",
                table: "Tickets",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AddressBooks");

            migrationBuilder.DropTable(
                name: "JournalExpendables");

            migrationBuilder.DropTable(
                name: "JournalMachines");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "MachinesConnects");

            migrationBuilder.DropTable(
                name: "MessageFiles");

            migrationBuilder.DropTable(
                name: "MessageTexts");

            migrationBuilder.DropTable(
                name: "NetworkAdresses");

            migrationBuilder.DropTable(
                name: "RemoveControls");

            migrationBuilder.DropTable(
                name: "TicketFiles");

            migrationBuilder.DropTable(
                name: "ExpendablesItems");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Expendables");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
