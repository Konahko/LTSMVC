using Microsoft.EntityFrameworkCore.Migrations;

namespace LTSMVC.Migrations
{
    public partial class addAdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ADName",
                table: "Staff",
                type: "varchar(55)",
                nullable: false,
                defaultValue: "",
                collation: "utf32_bin")
                .Annotation("MySql:CharSet", "utf32");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADName",
                table: "Staff");
        }
    }
}
