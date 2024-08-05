using System.ComponentModel.DataAnnotations;

namespace Montech.Web.DTOs;

public class UsuarioLoginDto
{
    [Required(ErrorMessage = "Informe o Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha")]
    public string Senha { get; set; } = string.Empty;
}
