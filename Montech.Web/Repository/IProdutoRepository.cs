using Montech.Web.Models;

namespace Montech.Web.Repository;

public interface IProdutoRepository
{
    Task<(List<ProdutoModel> produtos, int totalItens)> BuscarTodos(int pageNumber, int pageSize);
    Task<ProdutoModel> ListarPorId(long id);
    Task<ProdutoModel> Adicionar(ProdutoModel produto);
    Task<ProdutoModel> Atualizar(ProdutoModel produto);
    Task<bool> Apagar(long id);
}
