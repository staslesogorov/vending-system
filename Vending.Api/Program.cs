using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Vending.Api.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower, // или SnakeCaseUpper
    PropertyNameCaseInsensitive = true, // Опционально для дополнительной гибкости
};


app.MapGet(
    "/parsing",
    async (AppDbContext db) =>
    {   
        List<User> users = new();
        for (int i = 1; i < 20; i++)
        {
            var data = await File.ReadAllTextAsync($"users/{i}.json");
            var user = JsonSerializer.Deserialize<User>(data, options);
            Console.WriteLine(user!.FullName);
            users.Add(user);
        }

        try
        {
            await db.Users.AddRangeAsync(users);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Ошибка сохранения: {ex.InnerException!.Message}");
        }

        return users;
    }
);
app.MapGet(
    "/parsing_products",
    async (AppDbContext db) =>
    {
        var data = await File.ReadAllTextAsync("products_data/products.json");
        var products = JsonSerializer.Deserialize<Product[]>(data, options);
        System.Console.WriteLine(products?.Count());

        try
        {
            await db.Products.AddRangeAsync(products!);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Ошибка сохранения: {ex.InnerException!.Message}");
        }
    }
);



app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
);
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();