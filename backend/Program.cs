using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskFlow_API.Context;
using TaskFlow_API.Models;
using TaskFlow_API.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ConnectionString
// https://github.com/PedroSmaxY/SchedulingSystemAPI/blob/main/Program.cs
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
connectionString = connectionString?.Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

// Database Context (PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Identity (Users & Roles)
builder.Services.AddIdentityCore<Usuario>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!))
    });

// Authorization
builder.Services.AddAuthorization();

// Dependency Injection
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IEtiquetaService, EtiquetaService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Controllers
builder.Services.AddControllers();

// OpenAPI && Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seeder
Console.WriteLine("[Program.cs] Creando Admin y Roles");
using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.MigrateAsync();

    var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    Console.WriteLine("[Program.cs] Creando Roles");
    foreach (var role in new[] { "Admin", "User" })
    {
        if (!await roles.RoleExistsAsync(role))
        {
            await roles.CreateAsync(new IdentityRole(role));
        }
    }

    var admin = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
    var emailAdmin = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
    var passwordAdmin = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

    Console.WriteLine("[Program.cs] Creando Admin");
    var usuario = await admin.FindByEmailAsync(emailAdmin!);

    if (usuario == null)
    {
        var nuevoAdmin = new Usuario
        {
            UserName = emailAdmin,
            Email = emailAdmin,
            EmailConfirmed = true
        };

        var resultado = await admin.CreateAsync(nuevoAdmin, passwordAdmin!);

        if (!resultado.Succeeded)
        {
            Console.WriteLine("[Program.cs] No se pudo crear Administrador");
            Console.WriteLine(resultado.Errors.First().Description);
        }
        else
        {
            await admin.AddToRoleAsync(nuevoAdmin, "Admin");
            Console.WriteLine("[Program.cs] Administrador Creado Exitosamente");
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontendPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();