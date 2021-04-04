using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LTSMVC.Migrations
{
    public partial class reworkTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketFiles");

            migrationBuilder.DropColumn(
                name: "ToUser",
                table: "Messages");

            migrationBuilder.AddColumn<short>(
                name: "WorkerId",
                table: "Tickets",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "fk_ticketid_ticket_idx",
                table: "Messages",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "fk_ticketid_ticket",
                table: "Messages",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_ticketid_ticket",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "fk_ticketid_ticket_idx",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Messages");

            migrationBuilder.AddColumn<short>(
                name: "ToUser",
                table: "Messages",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "TicketFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataType = table.Column<string>(type: "varchar(8)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    TicketId = table.Column<int>(type: "int", nullable: false)
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
                name: "fk_ticket_file_Ticket1_idx",
                table: "TicketFiles",
                column: "TicketId");
        }
    }
}
