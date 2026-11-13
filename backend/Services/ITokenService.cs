using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public interface ITokenService
{
    public string CrearToken(Usuario usuario, List<string> roles);
}