using Montech.Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Montech.Web.Models;

public class ProdutoModel
{
    [Key]
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [MaxLength(25, ErrorMessage = "O tamanho do campo Nome não pode exceder 25 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(350, ErrorMessage = "O tamanho do campo Descricao não pode exceder 350 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Preencha a Data de compra")]
    public DateTime DataCompra { get; set; } = DateTime.Now.Date;

    [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
    public DateTime? DataVenda { get; set; }

    [DataType(DataType.Currency)]
    public decimal ValorCompra { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? ValorAtualDeMercado { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? ValorVenda { get; set; }

    //[Range(0, 3000)]
    //public int Quantidade { get; set; }

    [MaybeNull]
    public byte[]? Foto { get; set; }

    public EStatus Status { get; set; } = EStatus.Ativo;

    // Relacionamentos
    [Required(ErrorMessage = "Selecione uma categoria")]
    public string CategoriaNome { get; set; } = string.Empty; // Chave estrangeira para Categoria

    //[ValidateNever]
    public CategoriaModel? Categoria { get; set; }

    //[Required(ErrorMessage = "Selecione uma empresa")]
    public long? EmpresaId { get; set; } // Lógica para igualar produtos a empresa logada

    //[ValidateNever]
    public EmpresaModel? Empresa { get; set; }
}
