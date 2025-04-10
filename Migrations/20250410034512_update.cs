using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCore_Learning.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStock_AspNetUsers_User_id",
                table: "UserStock");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStock_Stock_Stock_id",
                table: "UserStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStock",
                table: "UserStock");

            migrationBuilder.RenameTable(
                name: "UserStock",
                newName: "Portfolios");

            migrationBuilder.RenameIndex(
                name: "IX_UserStock_User_id",
                table: "Portfolios",
                newName: "IX_Portfolios_User_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserStock_Stock_id",
                table: "Portfolios",
                newName: "IX_Portfolios_Stock_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios",
                column: "UserStock_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_AspNetUsers_User_id",
                table: "Portfolios",
                column: "User_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Stock_Stock_id",
                table: "Portfolios",
                column: "Stock_id",
                principalTable: "Stock",
                principalColumn: "Stock_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_AspNetUsers_User_id",
                table: "Portfolios");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Stock_Stock_id",
                table: "Portfolios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios");

            migrationBuilder.RenameTable(
                name: "Portfolios",
                newName: "UserStock");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolios_User_id",
                table: "UserStock",
                newName: "IX_UserStock_User_id");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolios_Stock_id",
                table: "UserStock",
                newName: "IX_UserStock_Stock_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStock",
                table: "UserStock",
                column: "UserStock_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStock_AspNetUsers_User_id",
                table: "UserStock",
                column: "User_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStock_Stock_Stock_id",
                table: "UserStock",
                column: "Stock_id",
                principalTable: "Stock",
                principalColumn: "Stock_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
