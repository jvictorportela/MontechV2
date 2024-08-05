using Montech.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace Montech.Web.Models;

public class UsuarioModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [MaxLength(55, ErrorMessage = "O tamanho do campo Nome não pode exceder 55 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Email é obrigatório")]
    [MaxLength(55, ErrorMessage = "O tamanho do campo Email não pode exceder 55 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo CPF é obrigatório")]
    [MaxLength(11, ErrorMessage = "O tamanho do campo CPF não pode exceder 11 caracteres"), MinLength(11, ErrorMessage = "O tamanho do campor CPF precisa conter 11 caracteres")]
    public string CPF { get; set; } = string.Empty;

    public byte[] SenhaHash { get; set; }
    public byte[] SenhaSalt { get; set; }


    public EStatus Status { get; set; } = EStatus.Ativo;
}
