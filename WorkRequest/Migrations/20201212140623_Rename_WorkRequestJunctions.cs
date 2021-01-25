using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkRequestManagment.Migrations
{
    public partial class Rename_WorkRequestJunctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkRequestUserJunction_Users_UserId",
                table: "WorkRequestUserJunction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkRequestUserJunction_WorkRequests_WorkRequestId",
                table: "WorkRequestUserJunction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkRequestUserJunction",
                table: "WorkRequestUserJunction");

            migrationBuilder.RenameTable(
                name: "WorkRequestUserJunction",
                newName: "WorkRequestUserJunctions");

            migrationBuilder.RenameIndex(
                name: "IX_WorkRequestUserJunction_WorkRequestId",
                table: "WorkRequestUserJunctions",
                newName: "IX_WorkRequestUserJunctions_WorkRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkRequestUserJunction_UserId",
                table: "WorkRequestUserJunctions",
                newName: "IX_WorkRequestUserJunctions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkRequestUserJunctions",
                table: "WorkRequestUserJunctions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRequestUserJunctions_Users_UserId",
                table: "WorkRequestUserJunctions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRequestUserJunctions_WorkRequests_WorkRequestId",
                table: "WorkRequestUserJunctions",
                column: "WorkRequestId",
                principalTable: "WorkRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkRequestUserJunctions_Users_UserId",
                table: "WorkRequestUserJunctions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkRequestUserJunctions_WorkRequests_WorkRequestId",
                table: "WorkRequestUserJunctions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkRequestUserJunctions",
                table: "WorkRequestUserJunctions");

            migrationBuilder.RenameTable(
                name: "WorkRequestUserJunctions",
                newName: "WorkRequestUserJunction");

            migrationBuilder.RenameIndex(
                name: "IX_WorkRequestUserJunctions_WorkRequestId",
                table: "WorkRequestUserJunction",
                newName: "IX_WorkRequestUserJunction_WorkRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkRequestUserJunctions_UserId",
                table: "WorkRequestUserJunction",
                newName: "IX_WorkRequestUserJunction_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkRequestUserJunction",
                table: "WorkRequestUserJunction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRequestUserJunction_Users_UserId",
                table: "WorkRequestUserJunction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRequestUserJunction_WorkRequests_WorkRequestId",
                table: "WorkRequestUserJunction",
                column: "WorkRequestId",
                principalTable: "WorkRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
