using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BOLMS.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_tblOrderStatus",
                columns: table => new
                {
                    idStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblOrderStatus", x => x.idStatus);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_tblCollection",
                columns: table => new
                {
                    idOrderCollected = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCollected = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNumberID = table.Column<int>(type: "int", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatusID = table.Column<int>(type: "int", nullable: false),
                    Driver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleReg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblCollection", x => x.idOrderCollected);
                    table.ForeignKey(
                        name: "FK__tblCollection__tblOrderStatus_OrderStatusID",
                        column: x => x.OrderStatusID,
                        principalTable: "_tblOrderStatus",
                        principalColumn: "idStatus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_tblCollectionLines",
                columns: table => new
                {
                    idOrderLineCollected = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCollectedID = table.Column<int>(type: "int", nullable: false),
                    ItemCodeID = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Warehouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtyPurchased = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtyCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblCollectionLines", x => x.idOrderLineCollected);
                    table.ForeignKey(
                        name: "FK__tblCollectionLines__tblCollection_OrderCollectedID",
                        column: x => x.OrderCollectedID,
                        principalTable: "_tblCollection",
                        principalColumn: "idOrderCollected",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "_tblOrderStatus",
                columns: new[] { "idStatus", "StatusCode", "StatusDescription" },
                values: new object[,]
                {
                    { 1, "Not Collected", "Not Collected" },
                    { 2, "Partially Collected", "Partially Collected" },
                    { 3, "Fully Collected", "Fully Collected" },
                    { 4, "Over Collected", "Over Collected" }
                });

            migrationBuilder.CreateIndex(
                name: "IX__tblCollection_OrderStatusID",
                table: "_tblCollection",
                column: "OrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX__tblCollectionLines_OrderCollectedID",
                table: "_tblCollectionLines",
                column: "OrderCollectedID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_tblCollectionLines");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "_tblCollection");

            migrationBuilder.DropTable(
                name: "_tblOrderStatus");
        }
    }
}
