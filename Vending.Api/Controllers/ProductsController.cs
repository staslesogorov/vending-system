using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Vending.Api.Data;
namespace Vending.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    [SwaggerOperation(
        Summary = "Список продуктов",
        Description = "Возвращает список продуктов"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Список продуктов", typeof(Product))]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return _context.Products.ToList();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Продукт",
        Description = "Возвращает продукт"
    )]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Продукт не найден")]
    [SwaggerResponse(StatusCodes.Status200OK, "Продукт", typeof(Product))]
    public ActionResult<Product> GetProduct(Guid id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        return product;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создать продукт",
        Description = "Создает продукт"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Продукт создан", typeof(Product))]
    public ActionResult<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Изменить продукт",
        Description = "Ищет продукт и изменяет его"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Продукт изменен")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Продукт не совпадает с айди")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Продукт не найден")]
    public IActionResult UpdateProduct(Guid id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Удалить продукт",
        Description = "Удаляет продукт"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Продукт удален")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Продукт не найден")]
    public IActionResult DeleteProduct(Guid id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        _context.SaveChanges();

        return NoContent();
    }
}
