using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class contex_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_AspNetUsers_UserId",
                table: "TaskList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_TaskList_TaskListId",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskList",
                table: "TaskList");

            migrationBuilder.RenameTable(
                name: "UserTask",
                newName: "UserTasks");

            migrationBuilder.RenameTable(
                name: "TaskList",
                newName: "TaskLists");

            migrationBuilder.RenameIndex(
                name: "IX_UserTask_TaskListId",
                table: "UserTasks",
                newName: "IX_UserTasks_TaskListId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskList_UserId",
                table: "TaskLists",
                newName: "IX_TaskLists_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TaskLists",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTasks",
                table: "UserTasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_AspNetUsers_UserId",
                table: "TaskLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_TaskLists_TaskListId",
                table: "UserTasks",
                column: "TaskListId",
                principalTable: "TaskLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_AspNetUsers_UserId",
                table: "TaskLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_TaskLists_TaskListId",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTasks",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists");

            migrationBuilder.RenameTable(
                name: "UserTasks",
                newName: "UserTask");

            migrationBuilder.RenameTable(
                name: "TaskLists",
                newName: "TaskList");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_TaskListId",
                table: "UserTask",
                newName: "IX_UserTask_TaskListId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskLists_UserId",
                table: "TaskList",
                newName: "IX_TaskList_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TaskList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskList",
                table: "TaskList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_AspNetUsers_UserId",
                table: "TaskList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_TaskList_TaskListId",
                table: "UserTask",
                column: "TaskListId",
                principalTable: "TaskList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
