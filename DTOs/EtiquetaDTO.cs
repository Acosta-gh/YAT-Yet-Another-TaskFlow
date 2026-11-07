using System.ComponentModel.DataAnnotations;

namespace TaskFlow_API.DTOs;

public class EtiquetaDTO
{
    [Required] [MaxLength(50)] public string Nombre { get; set; } = string.Empty;
}