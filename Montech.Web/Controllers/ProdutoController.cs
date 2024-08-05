using Microsoft.AspNetCore.Mvc;
using Montech.Web.Models;
using Montech.Web.Repository.Produto;
using Montech.Web.Repository.Sessao;
using Montech.Web.ViewModels;

namespace Montech.Web.Controllers;

public class ProdutoController : Controller
{
    private readonly IProdutoInterface _produtoRepository;
    private readonly ISessaoInterface _sessaoInterface;

    public ProdutoController(IProdutoInterface produtoRepository, ISessaoInterface sessaoInterface)
    {
        _produtoRepository = produtoRepository;
        _sessaoInterface = sessaoInterface;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }
        
        var (produtos, totalItems) = await _produtoRepository.BuscarTodos(pageNumber, pageSize);
        var viewModel = new ProdutoListViewModel
        {
            Produtos = produtos,
            Paginacao = new PaginacaoModel
            {
                PaginaAtual = pageNumber,
                TotalItens = totalItems,
                ItensPorPagina = pageSize
            }
        };
        return View(viewModel);
    }

    public IActionResult CriarProduto()
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarProduto(ProdutoModel produto) //Sobrecarga
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        await _produtoRepository.Adicionar(produto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditarProduto(long id)
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        var produto = await _produtoRepository.ListarPorId(id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> EditarProduto(ProdutoModel produto)
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        if (ModelState.IsValid)
        {
            await _produtoRepository.Atualizar(produto);
            
            return RedirectToAction("Index");
        }
        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirProduto(long id)
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        var produto = await _produtoRepository.ListarPorId(id);
        if (produto != null!)
        {
            await _produtoRepository.Apagar(id);
        }
        return RedirectToAction("Index");
    }

}
