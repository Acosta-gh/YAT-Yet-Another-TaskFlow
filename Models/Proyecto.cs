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

using System.ComponentModel.DataAnnotations;

namespace TaskFlow_API.Models;

public class Proyecto
{
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Nombre { get; set; } = string.Empty;

    public List<Tarea> Tareas { get; set; } = new List<Tarea>();
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}