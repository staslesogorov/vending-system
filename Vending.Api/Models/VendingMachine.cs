using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum PaymentType { Наличные, Карта, Оба }
public enum VendingStatus { Работает, ВышелИзСтроя, ВРемонте }
public enum Country { Россия, США, Китай, Германия }

public class VendingMachine
{
    public int Id { get; set; }

    [Required] public string Location { get; set; }
    [Required] public string Model { get; set; }
    [Required] public PaymentType Type { get; set; }
    [Range(0, double.MaxValue)] public decimal TotalIncome { get; set; }

    [Required] public string SerialNumber { get; set; }
    [Required] public string InventoryNumber { get; set; }

    [Required] public string Manufacturer { get; set; }
    [Required] public DateTime ManufactureDate { get; set; }
    [Required] public DateTime StartOperationDate { get; set; }

    [Required] public DateTime LastCalibrationDate { get; set; }
    public int? CalibrationIntervalMonths { get; set; }
    [Range(1, int.MaxValue)] public int ResourceHours { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    [Range(1, 20)] public int MaintenanceTimeHours { get; set; }

    [Required] public VendingStatus Status { get; set; }
    [Required] public Country CountryOfOrigin { get; set; }

    public DateTime? InventoryDate { get; set; }
    public string LastCalibrationBy { get; set; }

    public ICollection<Sale> Sales { get; set; }
    public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
}
