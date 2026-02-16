using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpsMax.Migrations
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
                name: "Drivers",
                columns: table => new
                {
                    idDriver = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.idDriver);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSources",
                columns: table => new
                {
                    idPaymentSource = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Account = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<int>(type: "int", nullable: false),
                    OrderReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaisedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCaptured = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSources", x => x.idPaymentSource);
                });

            migrationBuilder.CreateTable(
                name: "StkItm",
                columns: table => new
                {
                    StockLink = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description_1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StkItm", x => x.StockLink);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    idTruck = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CapacityTonnes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.idTruck);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    DCLink = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.DCLink);
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
                name: "PaymentSourceDocuments",
                columns: table => new
                {
                    idPaymentSourceDoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentSourceID = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSourceDocuments", x => x.idPaymentSourceDoc);
                    table.ForeignKey(
                        name: "FK_PaymentSourceDocuments_PaymentSources_PaymentSourceID",
                        column: x => x.PaymentSourceID,
                        principalTable: "PaymentSources",
                        principalColumn: "idPaymentSource",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loads",
                columns: table => new
                {
                    idLoad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DCLink = table.Column<int>(type: "int", nullable: false),
                    StockLink = table.Column<int>(type: "int", nullable: false),
                    idTruck = table.Column<int>(type: "int", nullable: false),
                    idDriver = table.Column<int>(type: "int", nullable: false),
                    LoadedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loads", x => x.idLoad);
                    table.ForeignKey(
                        name: "FK_Loads_Drivers_idDriver",
                        column: x => x.idDriver,
                        principalTable: "Drivers",
                        principalColumn: "idDriver",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loads_StkItm_StockLink",
                        column: x => x.StockLink,
                        principalTable: "StkItm",
                        principalColumn: "StockLink",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loads_Trucks_idTruck",
                        column: x => x.idTruck,
                        principalTable: "Trucks",
                        principalColumn: "idTruck",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loads_Vendor_DCLink",
                        column: x => x.DCLink,
                        principalTable: "Vendor",
                        principalColumn: "DCLink",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "CustomerAllocations",
                columns: table => new
                {
                    idCustomerAllocations = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuotationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllocatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAllocations", x => x.idCustomerAllocations);
                    table.ForeignKey(
                        name: "FK_CustomerAllocations_Loads_LoadID",
                        column: x => x.LoadID,
                        principalTable: "Loads",
                        principalColumn: "idLoad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoadDocuments",
                columns: table => new
                {
                    idLoadDocuments = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadID = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadDocuments", x => x.idLoadDocuments);
                    table.ForeignKey(
                        name: "FK_LoadDocuments_Loads_LoadID",
                        column: x => x.LoadID,
                        principalTable: "Loads",
                        principalColumn: "idLoad",
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

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAllocations_LoadID",
                table: "CustomerAllocations",
                column: "LoadID");

            migrationBuilder.CreateIndex(
                name: "IX_LoadDocuments_LoadID",
                table: "LoadDocuments",
                column: "LoadID");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_DCLink",
                table: "Loads",
                column: "DCLink");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_idDriver",
                table: "Loads",
                column: "idDriver");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_idTruck",
                table: "Loads",
                column: "idTruck");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_StockLink",
                table: "Loads",
                column: "StockLink");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSourceDocuments_PaymentSourceID",
                table: "PaymentSourceDocuments",
                column: "PaymentSourceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_tblCollectionLines");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CustomerAllocations");

            migrationBuilder.DropTable(
                name: "LoadDocuments");

            migrationBuilder.DropTable(
                name: "PaymentSourceDocuments");

            migrationBuilder.DropTable(
                name: "_tblCollection");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "PaymentSources");

            migrationBuilder.DropTable(
                name: "_tblOrderStatus");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "StkItm");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
