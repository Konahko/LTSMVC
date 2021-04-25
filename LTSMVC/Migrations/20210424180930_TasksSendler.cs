using Microsoft.EntityFrameworkCore.Migrations;

namespace LTSMVC.Migrations
{
    public partial class TasksSendler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Task",
                table: "Tasks",
                newName: "Job");

            migrationBuilder.AddColumn<short>(
                name: "TaskSendler",
                table: "Tasks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskSendler",
                table: "Tasks",
                column: "TaskSendler");

            migrationBuilder.AddForeignKey(
                name: "fr_Tasks_Staff1",
                table: "Tasks",
                column: "TaskSendler",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fr_Tasks_Staff1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskSendler",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskSendler",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Job",
                table: "Tasks",
                newName: "Task");
        }
    }
}
