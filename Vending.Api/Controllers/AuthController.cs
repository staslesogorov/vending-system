using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Vending.Api.Data;
using Vending.Api.Dto;

namespace Vending.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpPost]
    [SwaggerOperation(
        Summary = "Авторизация",
        Description = "Проверяет логин и пароль и возвращает пользователя"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Успешная авторизация", typeof(User))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Пользователь не найден или неверный пароль")]
    public IActionResult Auth(Dto.LoginRequest request)
    {
        var user = _context
            .Users.Where(u => u.Login == request.Login && u.Password == u.Password)
            .FirstOrDefault();

        if (user == null)
            return NotFound("Нет такого пользователя");

        bool valid = BCrypt.Net.BCrypt.Verify(request.Password, user!.Password);

        return valid ? Ok(user) : NotFound("Неправильный логин или пароль");
    }
}