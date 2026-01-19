using Microsoft.EntityFrameworkCore;

namespace Vending.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<VendingMachine> VendingMachines { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VendingMachine>(entity =>
        {
            entity.HasIndex(e => e.SerialNumber)
                .IsUnique()
                .HasDatabaseName("uniq_vendingmachine_serial_number");

            entity.HasIndex(e => e.InventoryNumber)
                .IsUnique()
                .HasDatabaseName("uniq_vendingmachine_inventory_number");


            entity.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "check_total_revenue_positive",
                    "\"TotalRevenue\" >= 0"
                );

                t.HasCheckConstraint(
                    "check_resource_hours_positive",
                    "\"ResourceHours\" > 0"
                );

                t.HasCheckConstraint(
                    "check_maintenance_duration_range",
                    "\"MaintenanceDurationHours\" BETWEEN 1 AND 20"
                );

                t.HasCheckConstraint(
                    "check_calibration_interval_positive",
                    "\"CalibrationIntervalMonths\" > 0"
                );

                t.HasCheckConstraint(
                    "check_commissioning_date",
                    "\"CommissioningDate\" >= \"ManufactureDate\" AND \"CommissioningDate\" <= CURRENT_DATE"
                );

                t.HasCheckConstraint(
                    "check_last_calibration_date",
                    "\"LastCalibrationDate\" >= \"ManufactureDate\" AND \"LastCalibrationDate\" <= CURRENT_DATE"
                );

                t.HasCheckConstraint(
                    "check_inventory_date",
                    "\"InventoryDate\" >= \"ManufactureDate\" AND \"InventoryDate\" <= CURRENT_DATE"
                );

                t.HasCheckConstraint(
                    "check_next_maintenance_date",
                    "\"NextMaintenanceDate\" > CURRENT_DATE"
                );
            });
        });
    }

}
