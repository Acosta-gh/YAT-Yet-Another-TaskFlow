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
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Enums;

namespace TaskFlow_API.Models;

public class Tarea
{
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Nombre { get; set; } = string.Empty;
    [MaxLength(250)] public string Descripcion { get; set; } = string.Empty;

    public List<Etiqueta> Etiquetas { get; set; } = new List<Etiqueta>();

    public int ProyectoId { get; set; }

    [JsonIgnore] public Proyecto Proyecto { get; set; } = null!;

    public string? UsuarioId { get; set; }

    public List<string> Colaboradores { get; set; } = new List<string>();

    [JsonIgnore]
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public Usuario? Usuario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;
    
    public Prioridad Prioridad { get; set; } = Prioridad.Baja;
}