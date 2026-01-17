using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }
    public string ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; } 
    public int MinimumStockLevel { get; set; }
    public string SalesTendency { get; set; }
}