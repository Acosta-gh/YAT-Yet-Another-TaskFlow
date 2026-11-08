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

public class TareaService : ITareaService
{
    private AppDbContext _context;

    public TareaService(AppDbContext context)
    {
        _context = context;
    }


    // DRY
    private async Task<List<Etiqueta>> ObtenerOCrearEtiquetasAsync(
        List<EtiquetaDTO> etiquetasDto)
    {
        // 1. Obtener los nombres recibidos de la Request.
        List<string> nombres = etiquetasDto
            .Select(e => e.Nombre)
            .ToList();

        // 2. Buscar etiquetas existentes.
        List<Etiqueta> etiquetasExistentes = await _context.Etiquetas
            .Where(e => nombres.Contains(e.Nombre))
            .ToListAsync();

        // 3. Identificar las etiquetas nuevas.
        List<string> nombresExistentes = etiquetasExistentes
            .Select(e => e.Nombre)
            .ToList();

        List<Etiqueta> etiquetasNuevas = nombres
            .Where(nombre => !nombresExistentes.Contains(nombre))
            .Select(nombre => new Etiqueta
            {
                Nombre = nombre
            })
            .ToList();

        // 4. Agregar las etiquetas nuevas a la DB.
        _context.Etiquetas.AddRange(etiquetasNuevas);

        // 5. Devolver todas las etiquetas.
        return etiquetasExistentes
            .Concat(etiquetasNuevas)
            .ToList();
    }

    public async Task<List<Tarea>> ObtenerTodosAsync()
    {
        return await _context.Tareas.Include(e => e.Etiquetas).ToListAsync();
    }

    public Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Tarea> CrearAsync(TareaDTO dto)
    {
        List<Etiqueta> etiquetas = await ObtenerOCrearEtiquetasAsync(dto.Etiquetas);

        Tarea t = new Tarea
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            ProyectoId = dto.ProyectoId,
            Etiquetas = etiquetas
        };

        _context.Tareas.Add(t);
        await _context.SaveChangesAsync();

        return t;
    }

    public async Task<bool> ActualizarAsync(int id, TareaDTO dto)
    {
        Tarea? tarea = await _context.Tareas
            .Include(t => t.Etiquetas)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tarea == null)
            return false;

        List<Etiqueta> etiquetas = await ObtenerOCrearEtiquetasAsync(dto.Etiquetas);

        tarea.Nombre = dto.Nombre;
        tarea.Descripcion = dto.Descripcion;
        tarea.ProyectoId = dto.ProyectoId;
        tarea.Etiquetas = etiquetas;

        await _context.SaveChangesAsync();

        return true;
    }

    public Task<bool> EliminarAsync(int id)
    {
        throw new NotImplementedException();
    }
}