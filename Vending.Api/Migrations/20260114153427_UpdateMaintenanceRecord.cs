using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Vending.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaintenanceRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "VendingMachines");

            migrationBuilder.DropColumn(
                name: "LastCheckDate",
                table: "VendingMachines");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ServiceStartDate",
                table: "VendingMachines",
                newName: "StartOperationDate");

            migrationBuilder.RenameColumn(
                name: "ServiceHours",
                table: "VendingMachines",
                newName: "MaintenanceTimeHours");

            migrationBuilder.RenameColumn(
                name: "NextServiceDate",
                table: "VendingMachines",
                newName: "NextMaintenanceDate");

            migrationBuilder.RenameColumn(
                name: "LastInventoryDate",
                table: "VendingMachines",
                newName: "InventoryDate");

            migrationBuilder.RenameColumn(
                name: "LastCheckEmployee",
                table: "VendingMachines",
                newName: "LastCalibrationBy");

            migrationBuilder.RenameColumn(
                name: "CheckIntervalMonths",
                table: "VendingMachines",
                newName: "CalibrationIntervalMonths");

            migrationBuilder.RenameColumn(
                name: "MinStock",
                table: "Products",
                newName: "MinimumStock");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "VendingMachines",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "VendingMachines",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "CountryOfOrigin",
                table: "VendingMachines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCalibrationDate",
                table: "VendingMachines",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethod",
                table: "Sales",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<decimal>(
                name: "PopularityScore",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "MaintenanceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendingMachineId = table.Column<int>(type: "integer", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkDescription = table.Column<string>(type: "text", nullable: false),
                    Problems = table.Column<string>(type: "text", nullable: false),
                    PerformedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_VendingMachines_VendingMachineId",
                        column: x => x.VendingMachineId,
                        principalTable: "VendingMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_VendingMachineId",
                table: "MaintenanceRecords",
                column: "VendingMachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "VendingMachines");

            migrationBuilder.DropColumn(
                name: "LastCalibrationDate",
                table: "VendingMachines");

            migrationBuilder.DropColumn(
                name: "PopularityScore",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "StartOperationDate",
                table: "VendingMachines",
                newName: "ServiceStartDate");

            migrationBuilder.RenameColumn(
                name: "NextMaintenanceDate",
                table: "VendingMachines",
                newName: "NextServiceDate");

            migrationBuilder.RenameColumn(
                name: "MaintenanceTimeHours",
                table: "VendingMachines",
                newName: "ServiceHours");

            migrationBuilder.RenameColumn(
                name: "LastCalibrationBy",
                table: "VendingMachines",
                newName: "LastCheckEmployee");

            migrationBuilder.RenameColumn(
                name: "InventoryDate",
                table: "VendingMachines",
                newName: "LastInventoryDate");

            migrationBuilder.RenameColumn(
                name: "CalibrationIntervalMonths",
                table: "VendingMachines",
                newName: "CheckIntervalMonths");

            migrationBuilder.RenameColumn(
                name: "MinimumStock",
                table: "Products",
                newName: "MinStock");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "VendingMachines",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "VendingMachines",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "VendingMachines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCheckDate",
                table: "VendingMachines",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Sales",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "Popularity",
                table: "Products",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExecutorId = table.Column<int>(type: "integer", nullable: true),
                    VendingMachineId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExecutorName = table.Column<string>(type: "text", nullable: false),
                    Issues = table.Column<string>(type: "text", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenances_Users_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintenances_VendingMachines_VendingMachineId",
                        column: x => x.VendingMachineId,
                        principalTable: "VendingMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_ExecutorId",
                table: "Maintenances",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_VendingMachineId",
                table: "Maintenances",
                column: "VendingMachineId");
        }
    }
}
