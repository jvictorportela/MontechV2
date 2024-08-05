using Microsoft.EntityFrameworkCore;
using Montech.Web.Data;
using Montech.Web.Models;
using Montech.Web.Repository.Produto;

namespace Montech.Web.Repository;

public class ProdutoService : IProdutoInterface
{
    private readonly AppDbContext _dbContext;

    public ProdutoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProdutoModel> Adicionar(ProdutoModel produto)
    {
        try
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();
            return produto;
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
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var produto = await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            if (produto == null)
            {
                return false;
            }

            _dbContext.Produtos.Remove(produto);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ProdutoModel> Atualizar(ProdutoModel produto)
    {
        try
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            var produtoExistente = await _dbContext.Produtos.FindAsync(produto.Id);

            if (produtoExistente == null)
            {
                throw new Exception("Produto não encontrado");
            }

            //produtoExistente.Id = produto.Id;
            //produtoExistente.Nome = produto.Nome;
            //produtoExistente.Descricao = produto.Descricao;
            //produtoExistente.DataCompra = produto.DataCompra;
            //produtoExistente.DataVenda = produto.DataVenda;
            //produtoExistente.ValorCompra = produto.ValorCompra;
            //produtoExistente.ValorAtualDeMercado = produto.ValorAtualDeMercado;
            //produtoExistente.ValorVenda = produto.ValorVenda;
            //produtoExistente.Foto = produto.Foto;
            //produtoExistente.Status = produto.Status;
            //produtoExistente.CategoriaNome = produto.CategoriaNome;

            _dbContext.Entry(produtoExistente).CurrentValues.SetValues(produto);

            await _dbContext.SaveChangesAsync();

            return produtoExistente;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<(List<ProdutoModel> produtos, int totalItens)> BuscarTodos(int pageNumber, int pageSize)
    {
        try
        {
            //List<ProdutoModel> listaDeProdutos = await _dbContext.Produtos.ToListAsync();
            //return listaDeProdutos;

            var query = _dbContext.Produtos.AsQueryable();
            int totalItems = await query.CountAsync();
            var produtos = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (produtos, totalItems);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ProdutoModel> ListarPorId(long id)
    {
        try
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
