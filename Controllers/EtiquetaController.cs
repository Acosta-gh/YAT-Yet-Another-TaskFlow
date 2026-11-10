/*
 * TaskFlow API - REST API for project and task management.
   Copyright (C) 2026 Cristian Darío Acosta
   
   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.
   
   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with this program. If not, see <https://www.gnu.org/licenses/>.
 */

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
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _etiquetaService.ObtenerTodosAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var etiqueta = await _etiquetaService.ObtenerPorIdAsync(id);
        if (etiqueta == null)
        {
            return NotFound();
        }

        return Ok(etiqueta);
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
    public async Task<IActionResult> Actualizar(int id, [FromBody] EtiquetaDTO dto)
    {
        var actualizado = await _etiquetaService.ActualizarAsync(id, dto);
        if (!actualizado)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _etiquetaService.EliminarAsync(id);
        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }
}
