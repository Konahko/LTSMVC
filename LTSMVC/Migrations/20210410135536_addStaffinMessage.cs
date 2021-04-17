using Microsoft.EntityFrameworkCore.Migrations;

namespace LTSMVC.Migrations
{
    public partial class addStaffinMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "fk_fromuser_staff_idx",
                table: "Messages",
                column: "FromUser");

            migrationBuilder.AddForeignKey(
                name: "fk_fromuser_staff",
                table: "Messages",
                column: "FromUser",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_fromuser_staff",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "fk_fromuser_staff_idx",
                table: "Messages");
        }
    }
}
