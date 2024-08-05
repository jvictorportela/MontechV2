using Microsoft.AspNetCore.Mvc;
using Montech.Web.Models;
using Montech.Web.Repository.ServicoPrestado;
using Montech.Web.Repository.Sessao;
using Montech.Web.ViewModels;

namespace Montech.Web.Controllers;

public class ServicoPrestadoController : Controller
{
    private readonly IServicoPrestadoInterface _servicoPrestadoRepository;
    private readonly ISessaoInterface _sessaoInterface;

    public ServicoPrestadoController(IServicoPrestadoInterface servicoPrestadoRepository, ISessaoInterface sessaoInterface)
    {
        _servicoPrestadoRepository = servicoPrestadoRepository;
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

        var (servicos, totalItems) = await _servicoPrestadoRepository.BuscarTodos(pageNumber, pageSize);

        var viewModel = new ServicosPrestadosListViewModel
        {
            Servicos = servicos,
            Paginacao = new Models.PaginacaoModel
            {
                PaginaAtual = pageNumber,
                TotalItens = totalItems,
                ItensPorPagina = pageSize
            }
        };
        return View(viewModel);
    }

    public IActionResult CriarServicoPrestado()
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
    public async Task<IActionResult> CriarServicoPrestado(ServicosPrestadosModel servico) //sobrecarga
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        await _servicoPrestadoRepository.Adicionar(servico);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditarServicoPrestado(long id)
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        var servico = await _servicoPrestadoRepository.ListarPorId(id);
        if (servico == null)
        {
            return NotFound();
        }

        return View(servico);
    }

    [HttpPost]
    public async Task<IActionResult> EditarServicoPrestado(ServicosPrestadosModel servico)
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        if (ModelState.IsValid)
        {
            await _servicoPrestadoRepository.Atualizar(servico);

            return RedirectToAction("Index");
        }

        // Log messages de erro de validação
        foreach (var modelState in ModelState.Values)
        {
            foreach (var error in modelState.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }

        return View(servico);
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirServicoPrestado(long id)
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario == null)
        {
            return RedirectToAction("Index", "Login");
        }

        var servico = await _servicoPrestadoRepository.ListarPorId(id);

        if(servico != null)
        {
            await _servicoPrestadoRepository.Apagar(id);
        }

        return RedirectToAction("Index");
    }
}
