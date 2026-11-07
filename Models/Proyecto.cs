using System.ComponentModel.DataAnnotations;

namespace TaskFlow_API.Models;

public class Proyecto
{
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Nombre { get; set; } = string.Empty;

    public List<Tarea> Tareas { get; set; } = new List<Tarea>();
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}