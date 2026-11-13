using System.ComponentModel.DataAnnotations;

namespace TaskFlow_API.DTOs;

public class RegisterDto
{
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = "";
    [Required]
    [MinLength(8)]
    [MaxLength(250)]
    public string Password { get; set; } = "";
}

public class LoginDto
{
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = "";
    
    [Required]
    [MinLength(8)]
    [MaxLength(250)]
    public string Password { get; set; } = "";
}

public class AuthResponseDto
{
    public string Token { get; set; } = "";
    public string Email { get; set; } = "";
    public List<string> Roles { get; set; } = new List<string>();
}
