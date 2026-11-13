using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace TaskFlow_API.Models;

public class Usuario : IdentityUser
{
    [JsonIgnore] public List<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    [JsonIgnore] public List<Tarea> Tareas { get; set; } = new List<Tarea>();
}