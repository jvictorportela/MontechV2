using Montech.Web.Data;
using Montech.Web.DTOs;
using Montech.Web.Models;
using Montech.Web.Repository.Senha;
using Montech.Web.Repository.Sessao;

namespace Montech.Web.Repository.Login;

public class LoginServices : ILoginInterface
{
    private readonly AppDbContext _context;
    private readonly ISenhaInterface _senhaInterface;
    private readonly ISessaoInterface _sessaoInterface;
    public LoginServices(AppDbContext context, ISenhaInterface senhaInterface, ISessaoInterface sessaoInterface)
    {
        _context = context;
        _senhaInterface = senhaInterface;
        _sessaoInterface = sessaoInterface;
    }

    public async Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto)
    {
        ResponseModel<UsuarioModel> responseModel = new(); 

        try
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == usuarioLoginDto.Email);

            if (usuario == null)
            {
                responseModel.Mensagem = "Credenciais inválidas";
                responseModel.Status = false;
                return responseModel;
            }

            if (!_senhaInterface.VerificarSenha(usuarioLoginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                responseModel.Mensagem = "Credenciais inválidas";
                responseModel.Status = false;
                return responseModel;
            }

            //Criar uma sessão
            _sessaoInterface.CriarSessao(usuario);

            responseModel.Mensagem = "Usuário logado com sucesso";
            responseModel.Status = true;
            return responseModel;
        }
        catch (Exception ex)
        {
            responseModel.Mensagem = ex.Message;
            responseModel.Status = false;
            return responseModel;
            throw new Exception(ex.Message);
        }
    }

    public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioDto)
    {
        ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

        try
        {
            if (VerificarEmailExiste(usuarioDto))
            {
                response.Mensagem = "Email já cadastrado";
                response.Status = false;
                return response;
            }

            _senhaInterface.CriarSenhaHash(usuarioDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            var usuario = new UsuarioModel()
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                CPF = usuarioDto.CPF,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt
            };

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            response.Mensagem = "Usuário cadastrado com sucesso";
            //Status já preenchido no Modelo
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
            return response;
        }
    }

    private bool VerificarEmailExiste(UsuarioRegisterDto usuarioDto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == usuarioDto.Email);

        if (usuario == null)
        {
            return false;
        }

        return true;
    }
}
