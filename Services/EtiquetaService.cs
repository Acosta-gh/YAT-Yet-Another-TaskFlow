using TaskFlow_API.Context;
using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public class EtiquetaService : IEtiquetaService
{
    private AppDbContext _dbContext;

    public EtiquetaService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Etiqueta>> ObtenerTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Etiqueta?> ObtenerPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Etiqueta> CrearAsync(EtiquetaDTO dto)
    {
        Etiqueta etiqueta = new Etiqueta
        {
            Nombre = dto.Nombre,
        };
        
        _dbContext.Etiquetas.Add(etiqueta);
        await _dbContext.SaveChangesAsync();

        return etiqueta;
    }

    public Task<bool> ActualizarAsync(int id, EtiquetaDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EliminarAsync(int id)
    {
        throw new NotImplementedException();
    }
}