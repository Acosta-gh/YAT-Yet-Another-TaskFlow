using TaskFlow_API.Context;
using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public class ProyectoService : IProyectoService
{
    private AppDbContext _context;

    public ProyectoService(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<List<Proyecto>> ObtenerTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Proyecto?> ObtenerPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Proyecto> CrearAsync(ProyectoDTO dto)
    {
        Proyecto p = new Proyecto { Nombre = dto.Nombre, };
        _context.Proyectos.Add(p);
        await _context.SaveChangesAsync();

        return p;
    }

    public Task<bool> ActualizarAsync(int id, ProyectoDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EliminarAsync(int id)
    {
        throw new NotImplementedException();
    }
}
