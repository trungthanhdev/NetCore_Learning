using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCore_Learning.Migrations
{
    /// <inheritdoc />
    public partial class InitSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Stock_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    Pruchase = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    LastDiv = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Industry = table.Column<string>(type: "text", nullable: false),
                    MarketCap = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Stock_id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StockId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Comment_id);
                    table.ForeignKey(
                        name: "FK_Comment_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Stock_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_StockId",
                table: "Comment",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
