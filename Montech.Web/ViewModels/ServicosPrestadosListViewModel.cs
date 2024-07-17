using Montech.Web.Models;

namespace Montech.Web.ViewModels;

public class ServicosPrestadosListViewModel
{
    public List<ServicosPrestadosModel> Servicos { get; set; }
    public PaginacaoModel Paginacao { get; set; }
}
