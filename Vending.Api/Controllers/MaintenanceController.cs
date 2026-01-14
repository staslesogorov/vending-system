using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vending.Api.Data;

namespace Vending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaintenanceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Maintenance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceRecord>>> GetMaintenanceRecords()
        {
            return await _context.MaintenanceRecords
                .Include(m => m.VendingMachine)
                .ToListAsync();
        }

        // GET: api/Maintenance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceRecord>> GetMaintenanceRecord(int id)
        {
            var record = await _context.MaintenanceRecords
                .Include(m => m.VendingMachine)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (record == null)
                return NotFound();

            return record;
        }

        // POST: api/Maintenance
        [HttpPost]
        public async Task<ActionResult<MaintenanceRecord>> CreateMaintenance(MaintenanceRecord record)
        {
            if (!_context.VendingMachines.Any(vm => vm.Id == record.VendingMachineId))
                return BadRequest("Аппарат не найден");

            _context.MaintenanceRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMaintenanceRecord), new { id = record.Id }, record);
        }

        // PUT: api/Maintenance/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenance(int id, MaintenanceRecord record)
        {
            if (id != record.Id)
                return BadRequest();

            _context.Entry(record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // DELETE: api/Maintenance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenance(int id)
        {
            var record = await _context.MaintenanceRecords.FindAsync(id);
            if (record == null)
                return NotFound();

            _context.MaintenanceRecords.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
