using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LTSMVC.Migrations
{
    public partial class addtask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_Message_text_Messages1",
                table: "MessageTexts");

            migrationBuilder.DropIndex(
                name: "fk_Message_text_Messages1_idx",
                table: "MessageTexts");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "MessageTexts",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "MessageTexts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "MessageTexts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(45)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Task = table.Column<string>(type: "varchar(500)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    DateOpen = table.Column<DateTime>(type: "timestamp(6)", nullable: false),
                    DateClose = table.Column<DateTime>(type: "timestamp(6)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffsTasks",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<short>(type: "smallint", nullable: false),
                    Task = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fr_StaffsTasks_Staff1",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fr_StaffsTasks_Task1",
                        column: x => x.Task,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_Message_text_Messages1_idx",
                table: "MessageTexts",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffsTasks_Id",
                table: "StaffsTasks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffsTasks_StaffId",
                table: "StaffsTasks",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffsTasks_Task",
                table: "StaffsTasks",
                column: "Task");

            migrationBuilder.CreateIndex(
                name: "Tasks_id_3",
                table: "Tasks",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_Message_text_Messages1",
                table: "MessageTexts",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_Message_text_Messages1",
                table: "MessageTexts");

            migrationBuilder.DropTable(
                name: "StaffsTasks");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "MessageTexts");

            migrationBuilder.DropIndex(
                name: "fk_Message_text_Messages1_idx",
                table: "MessageTexts");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "MessageTexts");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "MessageTexts",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "fk_Message_text_Messages1_idx",
                table: "MessageTexts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_Message_text_Messages1",
                table: "MessageTexts",
                column: "Id",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
