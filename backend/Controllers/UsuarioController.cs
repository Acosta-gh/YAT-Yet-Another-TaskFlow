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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Context;
using TaskFlow_API.DTOs;

namespace TaskFlow_API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User,Admin")]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        List<UsuarioDTO> usuarios = await _context.Users
            .Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Email = u.Email ?? string.Empty
            })
            .ToListAsync();

        return Ok(usuarios);
    }
}
