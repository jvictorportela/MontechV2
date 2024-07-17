using Microsoft.AspNetCore.Mvc;
using Montech.Web.Models;
using Montech.Web.Repository.ServicoPrestado;
using Montech.Web.ViewModels;

namespace Montech.Web.Controllers;

public class ServicoPrestadoController : Controller
{
    private readonly IServicoPrestadoRepository _servicoPrestadoRepository;

    public ServicoPrestadoController(IServicoPrestadoRepository servicoPrestadoRepository)
    {
        _servicoPrestadoRepository = servicoPrestadoRepository;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
    {
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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarServicoPrestado(ServicosPrestadosModel servico) //sobrecarga
    {
        await _servicoPrestadoRepository.Adicionar(servico);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditarServicoPrestado(long id)
    {
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
        var servico = await _servicoPrestadoRepository.ListarPorId(id);

        if(servico != null)
        {
            await _servicoPrestadoRepository.Apagar(id);
        }

        return RedirectToAction("Index");
    }
}
