using System.ComponentModel.DataAnnotations;

namespace TaskFlow_API.DTOs;

public class TareaDTO
{
    [Required] [MaxLength(50)] public string Nombre { get; set; } = string.Empty;

    [MaxLength(250)] public string Descripcion { get; set; } = string.Empty;

    public int ProyectoId { get; set; }

    public List<EtiquetaDTO> Etiquetas { get; set; } = new List<EtiquetaDTO>();
}