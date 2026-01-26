using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vending.Api.Data;

namespace Vending.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return _context.Users.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(Guid id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return NotFound();

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _context.Users.Add(user);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, User user)
    {
        if (id != user.Id)
            return BadRequest();

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }
}
