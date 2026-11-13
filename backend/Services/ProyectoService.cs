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

    public async Task<List<Proyecto>> ObtenerTodosAsync(string usuarioId)
    {
        List<Proyecto> proyectos = await _context.Proyectos
            .Include(p => p.Tareas)
            .ThenInclude(e => e.Etiquetas)
            .Where(p => p.UsuarioId == usuarioId || p.Colaboradores.Contains(usuarioId))
            .ToListAsync();

        return proyectos;
    }

    public async Task<Proyecto?> ObtenerPorIdAsync(int id, string usuarioId)
    {
        Proyecto? proyecto = await _context.Proyectos
            .Include(p => p.Tareas)
            .ThenInclude(e => e.Etiquetas)
            .FirstOrDefaultAsync(p => p.Id == id
                                      && (p.UsuarioId == usuarioId
                                          || p.Colaboradores.Contains(usuarioId)));

        return proyecto;
    }

    public async Task<Proyecto> CrearAsync(ProyectoDTO dto, string usuarioId)
    {
        Proyecto p = new Proyecto
        {
            Nombre = dto.Nombre,
            Estado = dto.Estado,
            Prioridad = dto.Prioridad,
            UsuarioId = usuarioId,
            Colaboradores = await ObtenerColaboradoresValidosAsync(dto.Colaboradores, usuarioId)
        };
        _context.Proyectos.Add(p);
        await _context.SaveChangesAsync();

        return p;
    }

    public async Task<bool> ActualizarAsync(int id, ProyectoDTO dto, string usuarioId)
    {
        Proyecto? p = await _context.Proyectos
            .FirstOrDefaultAsync(p => p.Id == id
                                      && (p.UsuarioId == usuarioId
                                          || p.Colaboradores.Contains(usuarioId)));

        if (p == null || dto.Nombre == null)
        {
            return false;
        }

        p.Nombre = dto.Nombre;
        p.Estado = dto.Estado;
        p.Prioridad = dto.Prioridad;
        p.Colaboradores = await ObtenerColaboradoresValidosAsync(dto.Colaboradores, p.UsuarioId);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int id, string usuarioId, bool esAdmin)
    {
        Proyecto? p = await _context.Proyectos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (p == null)
        {
            return false;
        }

        bool esDuenio = p.UsuarioId == usuarioId;

        if (!esDuenio && !esAdmin)
        {
            return false;
        }

        _context.Proyectos.Remove(p);
        await _context.SaveChangesAsync();

        return true;
    }
}