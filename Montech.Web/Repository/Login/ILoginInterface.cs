using Montech.Web.DTOs;
using Montech.Web.Models;

namespace Montech.Web.Repository.Login;

public interface ILoginInterface
{
    //Task para ser Async
    Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioDto);
    Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto);
}
