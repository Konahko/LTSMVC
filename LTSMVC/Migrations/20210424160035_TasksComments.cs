using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LTSMVC.Migrations
{
    public partial class TasksComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TasksComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Task = table.Column<int>(type: "int", nullable: false),
                    FromUser = table.Column<short>(type: "smallint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp(6)", nullable: false),
                    CommentText = table.Column<string>(type: "varchar(1500)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fr_TasksComments_Staff1",
                        column: x => x.FromUser,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fr_TasksComments_Task1",
                        column: x => x.Task,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TasksComments_FromUser",
                table: "TasksComments",
                column: "FromUser");

            migrationBuilder.CreateIndex(
                name: "IX_TasksComments_Id",
                table: "TasksComments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TasksComments_Task",
                table: "TasksComments",
                column: "Task");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasksComments");
        }
    }
}
