using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montech.Web.Models;
using Montech.Web.Repository;
using Montech.Web.ViewModels;

namespace Montech.Web.Controllers;

public class ProdutoController : Controller
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
    {
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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarProduto(ProdutoModel produto) //Sobrecarga
    {
        await _produtoRepository.Adicionar(produto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditarProduto(long id)
    {
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
        if (ModelState.IsValid)
        {
            await _produtoRepository.Atualizar(produto);
            
            return RedirectToAction("Index");
        }
        //else
        //{
        //    // Logar os erros de validação
        //    var errors = ModelState.Values.SelectMany(v => v.Errors);
        //    foreach (var error in errors)
        //    {
        //        Console.WriteLine(error.ErrorMessage); // Ou use um logger para salvar os erros
        //    }
        //}
        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirProduto(long id)
    {
        var produto = await _produtoRepository.ListarPorId(id);
        if (produto != null!)
        {
            await _produtoRepository.Apagar(id);
        }
        return RedirectToAction("Index");
    }

}
