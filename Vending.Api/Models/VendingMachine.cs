using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class VendingMachine
{
    public string Location { get; set; }
    public string Model { get; set; }
    public string Type { get; set; }
    public decimal TotalRevenue { get; set; }
    public string? SerialNumber { get; set; }
    [Key]
    public string InventoryId { get; set; }
    public string Manufacturer { get; set; }
    public DateTime ManufactureDate { get; set; }
    public DateTime CommissioningDate { get; set; }
    public DateTime LastCalibrationDate { get; set; }
    public int CalibrationIntervalMonths { get; set; }
    public int ResourceHours { get; set; }
    public DateTime NextMaintenanceDate { get; set; }
    public int MaintenanceDurationHours { get; set; }
    public string Status { get; set; }
    public string ProductionCountry { get; set; }
    public DateTime InventoryDate { get; set; }
    public string LastCalibration { get; set; }

}