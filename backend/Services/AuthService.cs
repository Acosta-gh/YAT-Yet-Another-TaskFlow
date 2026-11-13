using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.DTOs;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<Usuario> userManager, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _userManager = userManager;
    }

    public async Task<bool> Registrar(RegisterDto dto)
    {
        var existe = await _userManager.FindByEmailAsync(dto.Email);

        if (existe != null)
        {
            return false;
        }

        Usuario usuario = new Usuario { Email = dto.Email, UserName = dto.Email };
        var resultado = await _userManager.CreateAsync(usuario, dto.Password);

        if (!resultado.Succeeded)
        {
            return false;
        }

        var resultadoRol = await _userManager.AddToRoleAsync(usuario, "User");

        if (!resultadoRol.Succeeded)
        {
            return false;
        }

        return true;
    }

    public async Task<AuthResponseDto?> IniciarSesion(LoginDto dto)
    {
        var usuario = await _userManager.FindByEmailAsync(dto.Email);

        if (usuario == null)
        {
            return null;
        }

        if (!await _userManager.CheckPasswordAsync(usuario, dto.Password))
        {
            return null;
        }

        var roles = (await _userManager.GetRolesAsync(usuario)).ToList();

        var token = _tokenService.CrearToken(usuario, roles);

        var response = new AuthResponseDto
        {
            Token = token,
            Email = usuario.Email!,
            Roles = roles
        };

        return response;
    }
}