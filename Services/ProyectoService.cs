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

using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Context;
using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public class ProyectoService : IProyectoService
{
    private AppDbContext _context;

    public ProyectoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Proyecto>> ObtenerTodosAsync()
    {
        
        return await _context.Proyectos.Include(t => t.Tareas).ThenInclude(e=>e.Etiquetas).ToListAsync();
    }

    public Task<Proyecto?> ObtenerPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Proyecto> CrearAsync(ProyectoDTO dto)
    {
        Proyecto p = new Proyecto { Nombre = dto.Nombre, };
        _context.Proyectos.Add(p);
        await _context.SaveChangesAsync();

        return p;
    }

    public Task<bool> ActualizarAsync(int id, ProyectoDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EliminarAsync(int id)
    {
        throw new NotImplementedException();
    }
}