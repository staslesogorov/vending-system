using System;
using System.ComponentModel.DataAnnotations;

public class Sale
{
    public int Id { get; set; }

    public string? VendingMachineId { get; set; }
    public VendingMachine? VendingMachine { get; set; }

    public string ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
    public decimal SaleAmount { get; set; }
    public DateTime SaleDateTime { get; set; }
    public string PaymentMethodSale { get; set; }

}
