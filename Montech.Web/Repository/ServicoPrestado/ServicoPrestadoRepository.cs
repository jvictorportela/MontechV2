using Microsoft.EntityFrameworkCore;
using Montech.Web.Data;
using Montech.Web.Models;

namespace Montech.Web.Repository.ServicoPrestado;

public class ServicoPrestadoRepository : IServicoPrestadoRepository
{
    private readonly AppDbContext _dbContext;

    public ServicoPrestadoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServicosPrestadosModel> Adicionar(ServicosPrestadosModel servicoPrestado)
    {
        await _dbContext.ServicosPrestados.AddAsync(servicoPrestado);
        await _dbContext.SaveChangesAsync();
        return servicoPrestado;
    }

    public async Task<bool> Apagar(long id)
    {
        var servico = await _dbContext.ServicosPrestados.FirstOrDefaultAsync(x => x.Id == id);
        if (servico == null)
        {
            return false;
        }
        _dbContext.ServicosPrestados.Remove(servico);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ServicosPrestadosModel> Atualizar(ServicosPrestadosModel servicoPrestado)
    {
        var servicoExistente = await _dbContext.ServicosPrestados.FindAsync(servicoPrestado.Id);
        if (servicoExistente == null)
        {
            throw new Exception("Serviço nao encontrado");
        }

        _dbContext.Entry(servicoExistente).CurrentValues.SetValues(servicoPrestado);

        await _dbContext.SaveChangesAsync();
        return servicoExistente;
    }

    public async Task<(List<ServicosPrestadosModel> servicosPrestados, int totalItens)> BuscarTodos(int pageNumber, int pageSize)
    {
        var query = _dbContext.ServicosPrestados.AsQueryable();

        int totalItens = await query.CountAsync();

        var servicos = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return (servicos, totalItens);
    }

    public async Task<ServicosPrestadosModel> ListarPorId(long id)
    {
        return await _dbContext.ServicosPrestados.FirstOrDefaultAsync(x => x.Id == id);
    }
}
