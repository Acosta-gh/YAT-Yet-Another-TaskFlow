using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.DTOs;
using TaskFlow_API.Services;

namespace TaskFlow_API.Controllers;

[ApiController]
[Route($"api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Registrarse(RegisterDto usuario)
    {
        var creado = await _authService.Registrar(usuario);
        if (!creado)
        {
            return BadRequest();
        }

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> IniciarSesion(LoginDto usuario)
    {
        Console.WriteLine("[AuthController.cs] Entro a Login");
        
        var user = await _authService.IniciarSesion(usuario);
        Console.WriteLine(user == null ? "[AuthController.cs] USER ES NULL" : "[AuthController.cs] USER EXISTE");
        if (user == null)
            return Unauthorized();

        return Ok(user);
    }
}