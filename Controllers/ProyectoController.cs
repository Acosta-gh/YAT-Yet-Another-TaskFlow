using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.DTOs;
using TaskFlow_API.Services;

namespace TaskFlow_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectoController : ControllerBase
{
    private readonly IProyectoService _proyectoService;

    public ProyectoController(IProyectoService proyectoService)
    {
        _proyectoService = proyectoService;
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
    public async Task<IActionResult> Crear(ProyectoDTO dto)
    {
        var ProyectoCreado = await _proyectoService.CrearAsync(dto);
        
        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = ProyectoCreado.Id },
            ProyectoCreado
        );
    }

    [HttpPut("{id:int}")]
    public Task<IActionResult> Actualizar(int id, [FromBody] ProyectoDTO dto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:int}")]
    public Task<IActionResult> Eliminar(int id)
    {
        throw new NotImplementedException();
    }
}
