using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public interface IEtiquetaService
{
    Task<List<Etiqueta>> ObtenerTodosAsync();
    Task<Etiqueta?> ObtenerPorIdAsync(int id);
    Task<Etiqueta> CrearAsync(EtiquetaDTO dto);
    Task<bool> ActualizarAsync(int id, EtiquetaDTO dto);
    Task<bool> EliminarAsync(int id);
}
