using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vending.Api.Data;

namespace Vending.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaintenanceController : ControllerBase
{
    private readonly AppDbContext _context;

    public MaintenanceController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MaintenanceRecord>> GetMaintenanceRecords()
    {
        return _context.MaintenanceRecords.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<MaintenanceRecord> GetMaintenanceRecord(int id)
    {
        var record = _context.MaintenanceRecords.FirstOrDefault(m => m.Id == id);

        if (record == null)
            return NotFound();

        return record;
    }

    [HttpPost]
    public ActionResult<MaintenanceRecord> CreateMaintenance(MaintenanceRecord record)
    {
        if (!_context.VendingMachines.Any(vm => vm.Id == record.VendingMachineId))
            return BadRequest("Аппарат не найден");

        _context.MaintenanceRecords.Add(record);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetMaintenanceRecord), new { id = record.Id }, record);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMaintenance(int id, MaintenanceRecord record)
    {
        if (id != record.Id)
            return BadRequest();

        _context.Entry(record).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.MaintenanceRecords.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMaintenance(int id)
    {
        var record = _context.MaintenanceRecords.FirstOrDefault(m => m.Id == id);
        if (record == null)
            return NotFound();

        _context.MaintenanceRecords.Remove(record);
        _context.SaveChanges();

        return NoContent();
    }
}
