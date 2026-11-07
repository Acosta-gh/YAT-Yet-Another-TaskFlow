using TaskFlow_API.Context;
using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public class TareaService : ITareaService
{
    private AppDbContext _context;

    public TareaService(AppDbContext context)
    {
        _context = context;   
    }
    
    public Task<List<Tarea>> ObtenerTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Tarea> CrearAsync(TareaDTO dto)
    {
        Tarea t = new Tarea
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
        };
        
        _context.Tareas.Add(t);
        await _context.SaveChangesAsync();

        return t;
    }

    public Task<bool> ActualizarAsync(int id, TareaDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EliminarAsync(int id)
    {
        throw new NotImplementedException();
    }
}
