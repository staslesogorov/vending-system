using System;
using System.ComponentModel.DataAnnotations;

public class Sale
{
    public int Id { get; set; }

    public int VendingMachineId { get; set; }
    public VendingMachine VendingMachine { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Range(1, int.MaxValue)] public int Quantity { get; set; }
    [Range(0, double.MaxValue)] public decimal TotalAmount { get; set; }

    public DateTime SaleDateTime { get; set; }
    public PaymentType PaymentMethod { get; set; }
}
