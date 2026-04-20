using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransactions",
                columns: table => new
                {
                    InventoryTransactionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductionNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PONumber = table.Column<string>(type: "TEXT", nullable: false),
                    QuantityBefore = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityAfter = table.Column<int>(type: "INTEGER", nullable: false),
                    InventoryTransactionType = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DoneBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransactions", x => x.InventoryTransactionID);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_Inventories_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventories",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventories",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    InventoryQuantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventories", x => new { x.ProductID, x.InventoryID });
                    table.ForeignKey(
                        name: "FK_ProductInventories_Inventories_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventories",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInventories_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransactions",
                columns: table => new
                {
                    ProductTransactionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SONumber = table.Column<string>(type: "TEXT", nullable: false),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductionNumber = table.Column<string>(type: "TEXT", nullable: false),
                    QuantityBefore = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityAfter = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductTransactionType = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DoneBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransactions", x => x.ProductTransactionID);
                    table.ForeignKey(
                        name: "FK_ProductTransactions_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryID", "InventoryName", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Bike Seat", 2.0, 10 },
                    { 2, "Bike Body", 4.0, 20 },
                    { 3, "Bike Wheel", 6.0, 30 },
                    { 4, "Bike Pedal", 8.0, 40 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Price", "ProductName", "Quantity" },
                values: new object[,]
                {
                    { 1, 200.0, "Bike", 10 },
                    { 2, 400.0, "Car", 20 }
                });

            migrationBuilder.InsertData(
                table: "ProductInventories",
                columns: new[] { "InventoryID", "ProductID", "InventoryQuantity" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 2, 1, 20 },
                    { 3, 1, 20 },
                    { 4, 1, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_InventoryID",
                table: "InventoryTransactions",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_InventoryID",
                table: "ProductInventories",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_ProductID",
                table: "ProductTransactions",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryTransactions");

            migrationBuilder.DropTable(
                name: "ProductInventories");

            migrationBuilder.DropTable(
                name: "ProductTransactions");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
