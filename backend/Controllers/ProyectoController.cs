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

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.DTOs;
using TaskFlow_API.Services;

namespace TaskFlow_API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User,Admin")]
public class ProyectoController : ControllerBase
{
    private readonly IProyectoService _proyectoService;

    public ProyectoController(IProyectoService proyectoService)
    {
        _proyectoService = proyectoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var proyectos = await _proyectoService.ObtenerTodosAsync(usuarioId);

        if (proyectos == null)
        {
            return NotFound();
        }

        return Ok(proyectos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var proyecto = await _proyectoService.ObtenerPorIdAsync(id, usuarioId);
        if (proyecto == null)
        {
            return NotFound();
        }

        return Ok(proyecto);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(ProyectoDTO dto)
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var proyectoCreado = await _proyectoService.CrearAsync(dto, usuarioId);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = proyectoCreado.Id },
            proyectoCreado
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ProyectoDTO dto)
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var actualizado = await _proyectoService.ActualizarAsync(id, dto, usuarioId);
        if (!actualizado)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var esAdmin = User.IsInRole("Admin");
        var eliminado = await _proyectoService.EliminarAsync(id, usuarioId, esAdmin);
        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }
}