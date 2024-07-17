using Montech.Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Montech.Web.Models;

public class ServicosPrestadosModel
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
    [Required(ErrorMessage = "Preencha a Data de prestação do serviço")]
    public DateTime DataCriacaoServico { get; set; } = DateTime.Now.Date;

    [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Preencha a Data de prestação do serviço")]
    public DateTime? DataPrazoServico { get; set; }

    [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime? DataFinalizacaoServico { get; set; }

    [Required(ErrorMessage = "Informe o valor do serviço prestado")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorServico { get; set; }

    public EStatus Status { get; set; } = EStatus.Ativo;

    [Required(ErrorMessage = "Informe o nome do cliente/empresa contratante")]
    public string Cliente { get; set; } = string.Empty;
}
