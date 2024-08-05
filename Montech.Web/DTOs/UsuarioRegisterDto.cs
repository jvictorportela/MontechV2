using System.ComponentModel.DataAnnotations;

namespace Montech.Web.DTOs;

public class UsuarioRegisterDto
{
    [Required(ErrorMessage = "Digite o Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo CPF é obrigatório")]
    [MaxLength(11, ErrorMessage = "O tamanho do campo CPF não pode exceder 11 caracteres"), MinLength(11, ErrorMessage = "O tamanho do campor CPF precisa conter 11 caracteres")]
    public string CPF { get; set; } = string.Empty;

    [Required(ErrorMessage = "Digite o Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Digite o Senha")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme a senha"),
    Compare("Senha", ErrorMessage = "As senhas não são iguais")]
    public string ConfirmaSenha { get; set; } = string.Empty;
}
