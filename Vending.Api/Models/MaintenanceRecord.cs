using System;
using System.ComponentModel.DataAnnotations;

public class MaintenanceRecord
{
    public int Id { get; set; }

    public string? VendingMachineId { get; set; }
    public VendingMachine? VendingMachine { get; set; }

    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public string Problems { get; set; }

    public int MaintainerId { get; set; }
    public User Maintainer { get; set; }
}

