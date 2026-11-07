using System.ComponentModel.DataAnnotations;
using TaskFlow_API.Models;

namespace TaskFlow_API.DTOs;

public class ProyectoDTO
{
    [Required] [MaxLength(50)] public string Nombre { get; set; }
    
}