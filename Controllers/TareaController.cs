using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.DTOs;
using TaskFlow_API.Services;

namespace TaskFlow_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareaController : ControllerBase
{
    private readonly ITareaService _tareaService;

    public TareaController(ITareaService tareaService)
    {
        _tareaService = tareaService;
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
    public async Task<IActionResult> Crear(TareaDTO dto)
    {
        var tareaCreada = await _tareaService.CrearAsync(dto);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = tareaCreada.Id },
            tareaCreada
        );
    }

    [HttpPut("{id:int}")]
    public Task<IActionResult> Actualizar(int id, TareaDTO dto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:int}")]
    public Task<IActionResult> Eliminar(int id)
    {
        throw new NotImplementedException();
    }
}
