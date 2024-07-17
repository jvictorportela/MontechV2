using Montech.Web.Models;

namespace Montech.Web.Repository.ServicoPrestado;

public interface IServicoPrestadoRepository
{
    Task<(List<ServicosPrestadosModel> servicosPrestados, int totalItens)> BuscarTodos(int pageNumber, int pageSize);
    Task<ServicosPrestadosModel> ListarPorId(long id);
    Task<ServicosPrestadosModel> Adicionar(ServicosPrestadosModel servicoPrestado);
    Task<ServicosPrestadosModel> Atualizar(ServicosPrestadosModel servicoPrestado);
    Task<bool> Apagar(long id);
}
