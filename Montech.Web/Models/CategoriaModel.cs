using Montech.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace Montech.Web.Models;

public class CategoriaModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [MaxLength(25, ErrorMessage = "O tamanho do campo Nome não pode exceder 25 caracteres")]
    public string Nome { get; set; } = string.Empty;

    public EStatus Status { get; set; } = EStatus.Ativo;
}
