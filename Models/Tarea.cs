using System.ComponentModel.DataAnnotations;

namespace TaskFlow_API.Models;

public class Tarea
{
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Nombre { get; set; } = string.Empty;

    [MaxLength(250)] public string Descripcion { get; set; } = string.Empty;

    public List<Etiqueta> Etiquetas { get; set; } = new List<Etiqueta>();

    public int ProyectoId { get; set; }
    public Proyecto Proyecto { get; set; } = null!;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}