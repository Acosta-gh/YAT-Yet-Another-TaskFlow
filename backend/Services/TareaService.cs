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

using System.Text.RegularExpressions;
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

    private static List<string> NormalizarColaboradores(
        List<string>? colaboradores, string? usuarioId)
    {
        List<string> resultado = new List<string>();

        if (colaboradores == null)
        {
            return resultado;
        }

        foreach (string id in colaboradores)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                continue;
            }

            if (id == usuarioId)
            {
                continue;
            }

            if (resultado.Contains(id))
            {
                continue;
            }

            resultado.Add(id);
        }

        return resultado;
    }

    private async Task<List<string>> ObtenerColaboradoresValidosAsync(
        List<string>? colaboradores, string? usuarioId)
    {
        List<string> normalizados = NormalizarColaboradores(colaboradores, usuarioId);

        if (normalizados.Count == 0)
        {
            return normalizados;
        }

        List<string> existentes = await _context.Users
            .Where(u => normalizados.Contains(u.Id))
            .Select(u => u.Id)
            .ToListAsync();

        return existentes;
    }

    // DRY
    private async Task<List<Etiqueta>> ObtenerOCrearEtiquetasAsync(
        List<EtiquetaDTO> etiquetasDto)
    {
        // 1. Obtener los nombres recibidos de la Request.
        List<string> nombres = etiquetasDto
            // https://stackoverflow.com/questions/206717/how-do-i-replace-multiple-spaces-with-a-single-space-in-c
            .Select(e => Regex.Replace(e.Nombre.Trim(), @"\s+", " ")
                .ToLowerInvariant())
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

    public async Task<List<Tarea>> ObtenerTodosAsync(string usuarioId)
    {
        List<Tarea> tareas = await _context.Tareas
            .Include(e => e.Etiquetas)
            .Where(t => t.UsuarioId == usuarioId
                || t.Colaboradores.Contains(usuarioId))
            .ToListAsync();

        return tareas;
    }

    public async Task<Tarea?> ObtenerPorIdAsync(int id, string usuarioId)
    {
        // https://stackoverflow.com/questions/40360512/findasync-and-include-linq-statements
        Tarea? tarea = await _context.Tareas
            .Include(e => e.Etiquetas)
            .FirstOrDefaultAsync(i => i.Id == id
                && (i.UsuarioId == usuarioId
                    || i.Colaboradores.Contains(usuarioId)));

        return tarea;
    }

    public async Task<Tarea?> CrearAsync(TareaDTO dto, string usuarioId)
    {
        bool tieneAccesoProyecto = await _context.Proyectos.AnyAsync(p =>
            p.Id == dto.ProyectoId
            && (p.UsuarioId == usuarioId
                || p.Colaboradores.Contains(usuarioId)));

        if (!tieneAccesoProyecto)
        {
            return null;
        }

        List<Etiqueta> etiquetas = await ObtenerOCrearEtiquetasAsync(dto.Etiquetas);

        Tarea t = new Tarea
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            ProyectoId = dto.ProyectoId,
            Estado = dto.Estado,
            Prioridad = dto.Prioridad,
            UsuarioId = usuarioId,
            Colaboradores = await ObtenerColaboradoresValidosAsync(dto.Colaboradores, usuarioId),
            Etiquetas = etiquetas
        };

        _context.Tareas.Add(t);
        await _context.SaveChangesAsync();

        return t;
    }

    public async Task<bool> ActualizarAsync(int id, TareaDTO dto, string usuarioId)
    {
        Tarea? tarea = await _context.Tareas
            .Include(t => t.Etiquetas)
            .FirstOrDefaultAsync(t => t.Id == id
                && (t.UsuarioId == usuarioId
                    || t.Colaboradores.Contains(usuarioId)));

        if (tarea == null)
        {
            return false;
        }

        if (tarea.ProyectoId != dto.ProyectoId)
        {
            bool tieneAccesoProyecto = await _context.Proyectos.AnyAsync(p =>
                p.Id == dto.ProyectoId
                && (p.UsuarioId == usuarioId
                    || p.Colaboradores.Contains(usuarioId)));

            if (!tieneAccesoProyecto)
            {
                return false;
            }
        }

        List<Etiqueta> etiquetas = await ObtenerOCrearEtiquetasAsync(dto.Etiquetas);

        tarea.Nombre = dto.Nombre;
        tarea.Descripcion = dto.Descripcion;
        tarea.ProyectoId = dto.ProyectoId;
        tarea.Estado = dto.Estado;
        tarea.Prioridad = dto.Prioridad;
        tarea.Colaboradores = await ObtenerColaboradoresValidosAsync(dto.Colaboradores, tarea.UsuarioId);
        tarea.Etiquetas = etiquetas;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int id, string usuarioId, bool esAdmin)
    {
        // https://stackoverflow.com/questions/15227339/deleting-an-item-with-entity-framework
        Tarea? t = await _context.Tareas.FirstOrDefaultAsync(t => t.Id == id);
        if (t == null)
        {
            return false;
        }

        bool esDuenio = t.UsuarioId == usuarioId;

        if (!esDuenio && !esAdmin)
        {
            return false;
        }

        _context.Tareas.Remove(t);
        await _context.SaveChangesAsync();

        return true;
    }
}