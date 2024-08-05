using Microsoft.AspNetCore.Mvc;
using Montech.Web.DTOs;
using Montech.Web.Repository.Login;
using Montech.Web.Repository.Sessao;

namespace Montech.Web.Controllers;

public class LoginController : Controller
{
    private readonly ILoginInterface _loginInterface;
    private readonly ISessaoInterface _sessaoInterface;

    public LoginController(ILoginInterface loginInterface, ISessaoInterface sessaoInterface)
    {
        _loginInterface = loginInterface;
        _sessaoInterface = sessaoInterface;
    }

    public IActionResult Index()
    {
        var usuario = _sessaoInterface.BuscarSessao();

        //Verificando se o usuário está logado
        if (usuario != null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Logout()
    {
        _sessaoInterface.RemoveSessao();
        return RedirectToAction("Index");
    }

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(UsuarioRegisterDto usuarioDto)
    {
        if (ModelState.IsValid)
        {
            var usuario = await _loginInterface.RegistrarUsuario(usuarioDto);

            if (usuario.Status)
            {
                TempData["MensagemSucesso"] = usuario.Mensagem;
            }
            else
            {
                TempData["MensagemErro"] = usuario.Mensagem;
                return View(usuarioDto);
            }

            return RedirectToAction("Index");
        }
        else
        {
            return View(usuarioDto);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return View("index", usuarioLoginDto);
        }

        var usuario = await _loginInterface.Login(usuarioLoginDto);

        if (usuario.Status)
        {
            TempData["MensagemSucesso"] = usuario.Mensagem;
            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["MensagemErro"] = usuario.Mensagem;
            return View("index", usuarioLoginDto);
        }
    }
}
