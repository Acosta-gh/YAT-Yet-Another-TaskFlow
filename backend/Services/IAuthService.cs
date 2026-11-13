using TaskFlow_API.DTOs;

namespace TaskFlow_API.Services;

public interface IAuthService
{
    public Task<bool> Registrar(RegisterDto registerDto);
    public Task<AuthResponseDto?> IniciarSesion(LoginDto loginDto);
}