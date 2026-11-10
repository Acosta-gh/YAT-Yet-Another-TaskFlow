using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Context;
using TaskFlow_API.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// https://github.com/PedroSmaxY/SchedulingSystemAPI/blob/main/Program.cs
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
connectionString = connectionString.Replace("${DB_PASSWORD}",Environment.GetEnvironmentVariable("DB_PASSWORD"));

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IEtiquetaService, EtiquetaService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
    
app.UseAuthorization();

app.MapControllers();

app.Run();