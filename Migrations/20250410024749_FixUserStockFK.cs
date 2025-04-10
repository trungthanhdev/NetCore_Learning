using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCore_Learning.Migrations
{
    /// <inheritdoc />
    public partial class FixUserStockFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStock",
                columns: table => new
                {
                    UserStock_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Stock_id = table.Column<Guid>(type: "uuid", nullable: false),
                    User_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStock", x => x.UserStock_id);
                    table.ForeignKey(
                        name: "FK_UserStock_AspNetUsers_User_id",
                        column: x => x.User_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStock_Stock_Stock_id",
                        column: x => x.Stock_id,
                        principalTable: "Stock",
                        principalColumn: "Stock_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStock_Stock_id",
                table: "UserStock",
                column: "Stock_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserStock_User_id",
                table: "UserStock",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStock");
        }
    }
}
