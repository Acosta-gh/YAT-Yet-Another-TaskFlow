using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public interface ITareaService
{
    Task<List<Tarea>> ObtenerTodosAsync();
    Task<Tarea?> ObtenerPorIdAsync(int id);
    Task<Tarea> CrearAsync(TareaDTO dto);
    Task<bool> ActualizarAsync(int id, TareaDTO dto);
    Task<bool> EliminarAsync(int id);
}
