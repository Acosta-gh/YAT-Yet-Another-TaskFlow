using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public interface IProyectoService
{
    Task<List<Proyecto>> ObtenerTodosAsync();
    Task<Proyecto?> ObtenerPorIdAsync(int id);
    Task<Proyecto> CrearAsync(ProyectoDTO dto);
    Task<bool> ActualizarAsync(int id, ProyectoDTO dto);
    Task<bool> EliminarAsync(int id);
}
