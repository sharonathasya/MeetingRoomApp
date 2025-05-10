using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class reinit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblMasterRole_AccountRole_AccountRoleId",
                table: "TblMasterRole");

            migrationBuilder.DropIndex(
                name: "IX_TblMasterRole_AccountRoleId",
                table: "TblMasterRole");

            migrationBuilder.DropColumn(
                name: "AccountRoleId",
                table: "TblMasterRole");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RoleId",
                table: "AccountRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_TblMasterRole_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "TblMasterRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_TblMasterRole_RoleId",
                table: "AccountRole");

            migrationBuilder.DropIndex(
                name: "IX_AccountRole_RoleId",
                table: "AccountRole");

            migrationBuilder.AddColumn<int>(
                name: "AccountRoleId",
                table: "TblMasterRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblMasterRole_AccountRoleId",
                table: "TblMasterRole",
                column: "AccountRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMasterRole_AccountRole_AccountRoleId",
                table: "TblMasterRole",
                column: "AccountRoleId",
                principalTable: "AccountRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
