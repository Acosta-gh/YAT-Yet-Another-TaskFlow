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

public class EtiquetaService : IEtiquetaService
{
    private AppDbContext _dbContext;

    public EtiquetaService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Etiqueta>> ObtenerTodosAsync()
    {
        return await _dbContext.Etiquetas.ToListAsync();
    }

    public async Task<Etiqueta?> ObtenerPorIdAsync(int id)
    {
        return await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Etiqueta> CrearAsync(EtiquetaDTO dto)
    {
        Etiqueta etiqueta = new Etiqueta
        {
            Nombre = dto.Nombre,
        };
        
        _dbContext.Etiquetas.Add(etiqueta);
        await _dbContext.SaveChangesAsync();

        return etiqueta;
    }

    public async Task<bool> ActualizarAsync(int id, EtiquetaDTO dto)
    {
        Etiqueta? etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == id);
        if (etiqueta == null || dto.Nombre == null)
        {
            return false;
        }

        etiqueta.Nombre = dto.Nombre;
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        Etiqueta? etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == id);
        if (etiqueta == null)
        {
            return false;
        }

        _dbContext.Etiquetas.Remove(etiqueta);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}