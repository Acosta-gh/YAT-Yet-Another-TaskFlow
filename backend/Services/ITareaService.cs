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

using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public interface ITareaService
{
    Task<List<Tarea>> ObtenerTodosAsync(string usuarioId);
    Task<Tarea?> ObtenerPorIdAsync(int id, string usuarioId);
    Task<Tarea?> CrearAsync(TareaDTO dto, string usuarioId);
    Task<bool> ActualizarAsync(int id, TareaDTO dto, string usuarioId);
    Task<bool> EliminarAsync(int id, string usuarioId, bool esAdmin);
}
