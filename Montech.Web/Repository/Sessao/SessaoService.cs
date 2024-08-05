using Montech.Web.Models;
using Newtonsoft.Json;

namespace Montech.Web.Repository.Sessao;

public class SessaoService : ISessaoInterface
{
    private readonly IHttpContextAccessor _contextAccessor;

    public SessaoService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public UsuarioModel BuscarSessao()
    {
        try
        {
            var sessaoUsuario = _contextAccessor.HttpContext!.Session.GetString("sessaoUsuario");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario)!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void CriarSessao(UsuarioModel usuarioModel)
    {
        try
        {
            if (usuarioModel == null)
            {
                throw new ArgumentNullException(nameof(usuarioModel));
            }

            var usuarioJson = JsonConvert.SerializeObject(usuarioModel);

            _contextAccessor.HttpContext!.Session.SetString("sessaoUsuario", usuarioJson);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void RemoveSessao()
    {
        try
        {
            _contextAccessor.HttpContext!.Session.Remove("sessaoUsuario");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
