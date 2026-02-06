using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Vending.Api.Data;

namespace Vending.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaintenanceController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    [SwaggerOperation(
        Summary = "Список всех обслуживаний",
        Description = "Возвращает список всех обслуживаний"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Список обслуживаний получен", typeof(MaintenanceRecord))]
    public ActionResult<IEnumerable<MaintenanceRecord>> GetMaintenanceRecords()
    {
        return _context.MaintenanceRecords.ToList();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Запись обслуживания",
        Description = "Ищет запись обслуживания и возвращает ее"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Запись найдена", typeof(MaintenanceRecord))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Запись не найдена")]
    public ActionResult<MaintenanceRecord> GetMaintenanceRecord(Guid id)
    {
        var record = _context.MaintenanceRecords.FirstOrDefault(m => m.Id == id);

        if (record == null)
            return NotFound();

        return record;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создать запись обслуживания",
        Description = "Ищет запись обслуживания и возвращает ее"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Запись создана", typeof(MaintenanceRecord))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Апарат не найден")]
    public ActionResult<MaintenanceRecord> CreateMaintenance(MaintenanceRecord record)
    {
        if (!_context.VendingMachines.Any(vm => vm.Id == record.VendingMachineId))
            return BadRequest("Аппарат не найден");

        _context.MaintenanceRecords.Add(record);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetMaintenanceRecord), new { id = record.Id }, record);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Изменить запись обслуживания",
        Description = "Ищет запись обслуживания и изменяет ее"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Запись обслуживания изменена")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Запись обслуживания не совпадает с айди")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Запись обслуживания не найдена")]
    public IActionResult UpdateMaintenance(Guid id, MaintenanceRecord record)
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
    [SwaggerOperation(
        Summary = "Удалить запись обслуживания",
        Description = "Удаляет запись обслуживания"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Запись обслуживания удалена")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Запись обслуживания не найдена")]
    public IActionResult DeleteMaintenance(Guid id)
    {
        var record = _context.MaintenanceRecords.FirstOrDefault(m => m.Id == id);
        if (record == null)
            return NotFound();

        _context.MaintenanceRecords.Remove(record);
        _context.SaveChanges();

        return NoContent();
    }
}
