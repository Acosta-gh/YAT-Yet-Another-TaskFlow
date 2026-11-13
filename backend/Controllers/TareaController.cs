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
public class TareaController : ControllerBase
{
    private readonly ITareaService _tareaService;

    public TareaController(ITareaService tareaService)
    {
        _tareaService = tareaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return Ok(await _tareaService.ObtenerTodosAsync(usuarioId));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var tarea = await _tareaService.ObtenerPorIdAsync(id, usuarioId);
        if (tarea == null)
        {
            return NotFound();
        }

        return Ok(tarea);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(TareaDTO dto)
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var tareaCreada = await _tareaService.CrearAsync(dto, usuarioId);

        if (tareaCreada == null)
        {
            return NotFound();
        }

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = tareaCreada.Id },
            tareaCreada
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, TareaDTO dto)
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var actualizado = await _tareaService.ActualizarAsync(id, dto, usuarioId);
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
        var eliminado = await _tareaService.EliminarAsync(id, usuarioId, esAdmin);
        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }
}