using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Models;

namespace TaskFlow_API.Context;

public class AppDbContext : DbContext
{
    public DbSet<Proyecto> Proyectos { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    public DbSet<Etiqueta> Etiquetas { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

}