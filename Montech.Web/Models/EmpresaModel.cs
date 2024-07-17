using Montech.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace Montech.Web.Models;

public class EmpresaModel
{
    [Required(ErrorMessage = "O campo Id é obrigatório")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [MaxLength(25, ErrorMessage = "O tamanho do campo Nome não pode exceder 25 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo email é obrigatório")]
    [MaxLength(35, ErrorMessage = "O tamanho do campo Email não pode exceder 35 caracteres")]
    public string Email { get; set; } = string.Empty;

    public virtual ICollection<ProdutoModel>? Produtos { get; set; }

    public EStatus Status { get; set; } = EStatus.Ativo;
}
