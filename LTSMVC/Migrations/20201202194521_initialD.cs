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
                name: "expendables",
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
                name: "messages",
                columns: table => new
                {
                    idMessage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FromUser = table.Column<short>(type: "smallint", nullable: false),
                    ToUser = table.Column<short>(type: "smallint", nullable: false),
                    is_only_file = table.Column<sbyte>(type: "tinyint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idMessage);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Staff_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Staff_Name = table.Column<string>(type: "varchar(40)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Staff_Sub = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Staff_Poss = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    admin_u = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    place = table.Column<string>(type: "varchar(45)", nullable: true, collation: "utf32_bin")
                        .Annotation("MySql:CharSet", "utf32"),
                    tg_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Staff_id);
                });

            migrationBuilder.CreateTable(
                name: "message_file",
                columns: table => new
                {
                    idMessage_file = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Messages_idMessage = table.Column<int>(type: "int", nullable: false),
                    Data_type = table.Column<string>(type: "varchar(7)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idMessage_file);
                    table.ForeignKey(
                        name: "fk_Message_File_Messages1",
                        column: x => x.Messages_idMessage,
                        principalTable: "messages",
                        principalColumn: "idMessage",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "message_text",
                columns: table => new
                {
                    Messages_idMessage = table.Column<int>(type: "int", nullable: false),
                    Message_text = table.Column<string>(type: "varchar(1000)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_Message_text_Messages1",
                        column: x => x.Messages_idMessage,
                        principalTable: "messages",
                        principalColumn: "idMessage",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    idStaff = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<short>(type: "smallint", nullable: false),
                    account_type = table.Column<string>(type: "varchar(10)", nullable: false, collation: "utf32_bin")
                        .Annotation("MySql:CharSet", "utf32"),
                    login = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    pass = table.Column<string>(type: "varchar(16)", nullable: true, collation: "utf32_bin")
                        .Annotation("MySql:CharSet", "utf32"),
                    out_date = table.Column<DateTime>(type: "timestamp(6)", nullable: true),
                    add_info = table.Column<string>(type: "varchar(30)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idStaff);
                    table.ForeignKey(
                        name: "fk_account_staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Staff_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "address_book",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<short>(type: "smallint", nullable: true),
                    Ipnumber = table.Column<int>(type: "int", nullable: true),
                    Phonenumber = table.Column<int>(type: "int", nullable: true),
                    post = table.Column<string>(type: "varchar(60)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    place = table.Column<string>(type: "varchar(20)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    SLD = table.Column<string>(type: "char(2)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_Num_table_staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Staff_id",
                        onDelete: ReferentialAction.Restrict);
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
                    StafId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Expendables_items_Expendables1",
                        column: x => x.ExpendablesId,
                        principalTable: "expendables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Expendables_items_staff1",
                        column: x => x.StafId,
                        principalTable: "Staff",
                        principalColumn: "Staff_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Staffid = table.Column<short>(type: "smallint", nullable: false),
                    type = table.Column<string>(type: "varchar(10)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    name = table.Column<string>(type: "varchar(60)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    inv_number = table.Column<string>(type: "char(13)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Status = table.Column<string>(type: "varchar(10)", nullable: true, defaultValueSql: "'Склад'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    charecter = table.Column<string>(type: "varchar(100)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    mod = table.Column<string>(type: "varchar(50)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    add_info = table.Column<string>(type: "varchar(45)", nullable: true, collation: "utf8_general_ci")
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
                        column: x => x.Staffid,
                        principalTable: "Staff",
                        principalColumn: "Staff_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id_ticket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Staff = table.Column<short>(type: "smallint", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    TicketProblem = table.Column<string>(type: "varchar(500)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Date_open = table.Column<DateTime>(type: "timestamp(6)", nullable: false),
                    Date_close = table.Column<DateTime>(type: "timestamp(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_ticket);
                    table.ForeignKey(
                        name: "fk_chat_staff1",
                        column: x => x.Staff,
                        principalTable: "Staff",
                        principalColumn: "Staff_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "journal_expendables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExpendablesItemsId = table.Column<int>(type: "int", nullable: false),
                    TrigerUser = table.Column<int>(type: "int", nullable: true),
                    state = table.Column<string>(type: "varchar(900)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    time = table.Column<DateTime>(type: "timestamp(6)", nullable: true)
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
                name: "journal_machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MachinesId = table.Column<short>(type: "smallint", nullable: false),
                    TrigerUser = table.Column<int>(type: "int", nullable: true),
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
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "license",
                columns: table => new
                {
                    idtLicense = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PRIMARY", x => x.idtLicense);
                    table.ForeignKey(
                        name: "fk_License_Machines1",
                        column: x => x.MachinesId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_License_staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Staff_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "machines_connect",
                columns: table => new
                {
                    idMachines_connect = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Machines_id = table.Column<short>(type: "smallint", nullable: false),
                    Login = table.Column<string>(type: "varchar(15)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Pass = table.Column<string>(type: "varchar(16)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Is_Admin = table.Column<sbyte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idMachines_connect);
                    table.ForeignKey(
                        name: "fk_Machines_connect_Machines1",
                        column: x => x.Machines_id,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "network_adress",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    Network = table.Column<short>(type: "smallint", nullable: false),
                    Ip_Adress = table.Column<short>(type: "smallint", nullable: true),
                    Machines_id_Machines = table.Column<short>(type: "smallint", nullable: true),
                    MAC = table.Column<string>(type: "varchar(17)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Address_type = table.Column<string>(type: "char(1)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_Network_adress_Machines1",
                        column: x => x.Machines_id_Machines,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "remove_control",
                columns: table => new
                {
                    idconnected_machines = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Machines_id = table.Column<short>(type: "smallint", nullable: false),
                    AnyDesk = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    AnyDesk_pass = table.Column<string>(type: "varchar(20)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    TeamViewer = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    TeamViewer_pass = table.Column<string>(type: "varchar(20)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Ammy_Admin = table.Column<string>(type: "varchar(10)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Ammy_Admin_pass = table.Column<string>(type: "varchar(20)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    rdp = table.Column<string>(type: "varchar(45)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idconnected_machines);
                    table.ForeignKey(
                        name: "fk_connected_machines_Machines1",
                        column: x => x.Machines_id,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ticket_file",
                columns: table => new
                {
                    id_message_file = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ticket_id_ticket = table.Column<int>(type: "int", nullable: false),
                    data_type = table.Column<string>(type: "varchar(8)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    name = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_message_file);
                    table.ForeignKey(
                        name: "fk_ticket_file_Ticket1",
                        column: x => x.Ticket_id_ticket,
                        principalTable: "ticket",
                        principalColumn: "id_ticket",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_account_staff1_idx",
                table: "account",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "account",
                column: "idStaff",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_Num_table_staff1_idx",
                table: "address_book",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "fk_Expendables_items_Expendables1_idx",
                table: "ExpendablesItems",
                column: "ExpendablesId");

            migrationBuilder.CreateIndex(
                name: "fk_Expendables_items_staff1_idx",
                table: "ExpendablesItems",
                column: "StafId");

            migrationBuilder.CreateIndex(
                name: "fk_journal_Expendables_Expendables_items1_idx",
                table: "journal_expendables",
                column: "ExpendablesItemsId");

            migrationBuilder.CreateIndex(
                name: "fk_Journal_Machines_Machines1_idx",
                table: "journal_machines",
                column: "MachinesId");

            migrationBuilder.CreateIndex(
                name: "fk_License_Machines1_idx",
                table: "license",
                column: "MachinesId");

            migrationBuilder.CreateIndex(
                name: "fk_License_staff1_idx",
                table: "license",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "fk_Machines_staff_index",
                table: "Machine",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "id_Machines_UNIQUE",
                table: "Machine",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Machine_Staffid",
                table: "Machine",
                column: "Staffid");

            migrationBuilder.CreateIndex(
                name: "fk_Machines_connect_Machines1_idx",
                table: "machines_connect",
                column: "Machines_id");

            migrationBuilder.CreateIndex(
                name: "fk_Message_File_Messages1_idx",
                table: "message_file",
                column: "Messages_idMessage");

            migrationBuilder.CreateIndex(
                name: "fk_Message_text_Messages1_idx",
                table: "message_text",
                column: "Messages_idMessage");

            migrationBuilder.CreateIndex(
                name: "fk_Network_adress_Machines1_idx",
                table: "network_adress",
                column: "Machines_id_Machines");

            migrationBuilder.CreateIndex(
                name: "fk_connected_machines_Machines1_idx",
                table: "remove_control",
                column: "Machines_id");

            migrationBuilder.CreateIndex(
                name: "Staff_id_3",
                table: "Staff",
                column: "Staff_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_chat_staff1_idx",
                table: "ticket",
                column: "Staff");

            migrationBuilder.CreateIndex(
                name: "fk_ticket_file_Ticket1_idx",
                table: "ticket_file",
                column: "Ticket_id_ticket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "address_book");

            migrationBuilder.DropTable(
                name: "journal_expendables");

            migrationBuilder.DropTable(
                name: "journal_machines");

            migrationBuilder.DropTable(
                name: "license");

            migrationBuilder.DropTable(
                name: "machines_connect");

            migrationBuilder.DropTable(
                name: "message_file");

            migrationBuilder.DropTable(
                name: "message_text");

            migrationBuilder.DropTable(
                name: "network_adress");

            migrationBuilder.DropTable(
                name: "remove_control");

            migrationBuilder.DropTable(
                name: "ticket_file");

            migrationBuilder.DropTable(
                name: "ExpendablesItems");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "expendables");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
