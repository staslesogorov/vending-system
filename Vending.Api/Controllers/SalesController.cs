using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vending.Api.Data;

namespace Vending.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SalesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Sale>> GetSales()
    {
        return _context.Sales.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Sale> GetSale(int id)
    {
        var sale = _context
            .Sales.Include(s => s.VendingMachineId)
            .Include(s => s.ProductId)
            .FirstOrDefault(s => s.Id == id);

        if (sale == null)
            return NotFound();

        return sale;
    }

    [HttpPost]
    public ActionResult<Sale> CreateSale(Sale sale)
    {
        if (!_context.VendingMachines.Any(vm => vm.Id == sale.VendingMachineId))
            return BadRequest("Аппарат не найден");

        if (!_context.Products.Any(p => p.Id == sale.ProductId))
            return BadRequest("Товар не найден");

        _context.Sales.Add(sale);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSale(int id, Sale sale)
    {
        if (id != sale.Id)
            return BadRequest();

        _context.Entry(sale).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Sales.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSale(int id)
    {
        var sale = _context.Sales.FirstOrDefault(s => s.Id == id);
        if (sale == null)
            return NotFound();

        _context.Sales.Remove(sale);
        _context.SaveChanges();

        return NoContent();
    }
}
