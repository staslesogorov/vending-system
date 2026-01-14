using System;
using System.ComponentModel.DataAnnotations;

public class MaintenanceRecord
{
    public int Id { get; set; }

    public int VendingMachineId { get; set; }
    public VendingMachine VendingMachine { get; set; }

    public DateTime MaintenanceDate { get; set; }
    public string WorkDescription { get; set; }
    public string Problems { get; set; }

    [Required] public string PerformedBy { get; set; }
}
