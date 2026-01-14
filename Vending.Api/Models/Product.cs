using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }
    
    [Required] public string Name { get; set; }
    public string Description { get; set; }
    
    [Range(0, double.MaxValue)] public decimal Price { get; set; }
    [Range(0, int.MaxValue)] public int Quantity { get; set; }
    [Range(0, int.MaxValue)] public int MinimumStock { get; set; }
    
    public decimal PopularityScore { get; set; }

    public ICollection<Sale> Sales { get; set; }
}
