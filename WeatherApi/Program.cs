using Microsoft.EntityFrameworkCore;
using WeatherApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost; Database=TestDemo; User Id=SA; Password=P@rni@n78; MultipleActiveResultSets=true; Encrypt=false";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapControllers();

app.Run("http://0.0.0.0:9779");
