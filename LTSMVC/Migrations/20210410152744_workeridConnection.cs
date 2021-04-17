using Microsoft.EntityFrameworkCore.Migrations;

namespace LTSMVC.Migrations
{
    public partial class workeridConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chat_staff1",
                table: "Tickets");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_WorkerId",
                table: "Tickets",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "fk_workerid_staff2",
                table: "Tickets",
                column: "WorkerId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_workerid_staff2",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_WorkerId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "fk_chat_staff1",
                table: "Tickets",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
