using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.DTOs;
using TaskFlow_API.Services;

namespace TaskFlow_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EtiquetaController : ControllerBase
{
    private readonly IEtiquetaService _etiquetaService;

    public EtiquetaController(IEtiquetaService etiquetaService)
    {
        _etiquetaService = etiquetaService;
    }

    [HttpGet]
    public Task<IActionResult> ObtenerTodos()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:int}")]
    public Task<IActionResult> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] EtiquetaDTO dto)
    {
        var etiquetaCreada = await _etiquetaService.CrearAsync(dto);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = etiquetaCreada.Id },
            etiquetaCreada
        );
    }

    [HttpPut("{id:int}")]
    public Task<IActionResult> Actualizar(int id, [FromBody] EtiquetaDTO dto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:int}")]
    public Task<IActionResult> Eliminar(int id)
    {
        throw new NotImplementedException();
    }
}
