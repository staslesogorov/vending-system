using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Vending.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    MinimumStockLevel = table.Column<int>(type: "integer", nullable: false),
                    SalesTendency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FIO = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendingMachines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "numeric", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    InventoryNumber = table.Column<string>(type: "text", nullable: true),
                    Manufacturer = table.Column<string>(type: "text", nullable: false),
                    ManufactureDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CommissioningDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastCalibrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CalibrationIntervalMonths = table.Column<int>(type: "integer", nullable: false),
                    ResourceHours = table.Column<int>(type: "integer", nullable: false),
                    NextMaintenanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaintenanceDurationHours = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ProductionCountry = table.Column<string>(type: "text", nullable: false),
                    InventoryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendingMachines", x => x.Id);
                    table.CheckConstraint("check_calibration_interval_positive", "\"CalibrationIntervalMonths\" > 0");
                    table.CheckConstraint("check_commissioning_date", "\"CommissioningDate\" >= \"ManufactureDate\" AND \"CommissioningDate\" <= CURRENT_DATE");
                    table.CheckConstraint("check_inventory_date", "\"InventoryDate\" >= \"ManufactureDate\" AND \"InventoryDate\" <= CURRENT_DATE");
                    table.CheckConstraint("check_last_calibration_date", "\"LastCalibrationDate\" >= \"ManufactureDate\" AND \"LastCalibrationDate\" <= CURRENT_DATE");
                    table.CheckConstraint("check_maintenance_duration_range", "\"MaintenanceDurationHours\" BETWEEN 1 AND 20");
                    table.CheckConstraint("check_next_maintenance_date", "\"NextMaintenanceDate\" > \"ManufactureDate\"");
                    table.CheckConstraint("check_resource_hours_positive", "\"ResourceHours\" > 0");
                    table.CheckConstraint("check_total_revenue_positive", "\"TotalRevenue\" >= 0");
                    table.ForeignKey(
                        name: "FK_VendingMachines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendingMachineId = table.Column<int>(type: "integer", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Problems = table.Column<string>(type: "text", nullable: false),
                    MaintainerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Users_MaintainerId",
                        column: x => x.MaintainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_VendingMachines_VendingMachineId",
                        column: x => x.VendingMachineId,
                        principalTable: "VendingMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendingMachineId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SaleAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    SaleDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethodSale = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_VendingMachines_VendingMachineId",
                        column: x => x.VendingMachineId,
                        principalTable: "VendingMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_MaintainerId",
                table: "MaintenanceRecords",
                column: "MaintainerId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_VendingMachineId",
                table: "MaintenanceRecords",
                column: "VendingMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_VendingMachineId",
                table: "Sales",
                column: "VendingMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_VendingMachines_UserId",
                table: "VendingMachines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "uniq_vendingmachine_inventory_number",
                table: "VendingMachines",
                column: "InventoryNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uniq_vendingmachine_serial_number",
                table: "VendingMachines",
                column: "SerialNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceRecords");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "VendingMachines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
