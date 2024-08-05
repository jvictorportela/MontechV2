using Microsoft.EntityFrameworkCore;
using Montech.Web.Data;
using Montech.Web.Models;

namespace Montech.Web.Repository.ServicoPrestado;

public class ServicoPrestadoService : IServicoPrestadoInterface
{
    private readonly AppDbContext _dbContext;

    public ServicoPrestadoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServicosPrestadosModel> Adicionar(ServicosPrestadosModel servicoPrestado)
    {
        try
        {
            if (servicoPrestado == null)
            {
                throw new ArgumentNullException(nameof(servicoPrestado));
            }

            await _dbContext.ServicosPrestados.AddAsync(servicoPrestado);
            await _dbContext.SaveChangesAsync();
            return servicoPrestado;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Apagar(long id)
    {
        try
        {
            if (id <=0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var servico = await _dbContext.ServicosPrestados.FirstOrDefaultAsync(x => x.Id == id);

            if (servico == null)
            {
                return false;
            }

            _dbContext.ServicosPrestados.Remove(servico);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ServicosPrestadosModel> Atualizar(ServicosPrestadosModel servicoPrestado)
    {
        try
        {
            if (servicoPrestado == null)
            {
                throw new ArgumentNullException(nameof(servicoPrestado));
            }

            var servicoExistente = await _dbContext.ServicosPrestados.FindAsync(servicoPrestado.Id);

            if (servicoExistente == null)
            {
                throw new Exception("Serviço nao encontrado");
            }

            _dbContext.Entry(servicoExistente).CurrentValues.SetValues(servicoPrestado);

            await _dbContext.SaveChangesAsync();
            return servicoExistente;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<(List<ServicosPrestadosModel> servicosPrestados, int totalItens)> BuscarTodos(int pageNumber, int pageSize)
    {
        try
        {
            var query = _dbContext.ServicosPrestados.AsQueryable();

            int totalItens = await query.CountAsync();

            var servicos = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (servicos, totalItens);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<ServicosPrestadosModel> ListarPorId(long id)
    {
        try
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _dbContext.ServicosPrestados.FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
