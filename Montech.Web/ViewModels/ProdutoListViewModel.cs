using Montech.Web.Models;

namespace Montech.Web.ViewModels;

public class ProdutoListViewModel
{
    public List<ProdutoModel> Produtos { get; set; }
    public PaginacaoModel Paginacao { get; set; }
}
