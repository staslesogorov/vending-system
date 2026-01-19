using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vending.Api.Data;

namespace Vending.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendingMachinesController : ControllerBase
{
    private readonly AppDbContext _context;

    public VendingMachinesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VendingMachine>> GetVendingMachines()
    {
        return _context.VendingMachines.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<VendingMachine> GetVendingMachine(int id)
    {
        var machine = _context.VendingMachines.FirstOrDefault(m => m.Id == id);

        if (machine == null)
        {
            return NotFound();
        }

        return machine;
    }

    [HttpPost]
    public ActionResult<VendingMachine> CreateVendingMachine(VendingMachine machine)
    {
        if (_context.VendingMachines.Any(m => m.SerialNumber == machine.SerialNumber))
            return BadRequest("ТА с таким серийным номером уже существует");

        if (_context.VendingMachines.Any(m => m.Id == machine.Id))
            return BadRequest("ТА с таким инвентарным номером уже существует");

        _context.VendingMachines.Add(machine);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetVendingMachine), new { id = machine.Id }, machine);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateVendingMachine(int id, VendingMachine machine)
    {
        if (id != machine.Id)
        {
            return BadRequest();
        }

        _context.Entry(machine).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.VendingMachines.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteVendingMachine(int id)
    {
        var machine = _context.VendingMachines.FirstOrDefault(m => m.Id == id);
        if (machine == null)
        {
            return NotFound();
        }

        _context.VendingMachines.Remove(machine);
        _context.SaveChanges();

        return NoContent();
    }
}
