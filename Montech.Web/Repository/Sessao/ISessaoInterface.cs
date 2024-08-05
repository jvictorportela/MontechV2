using Montech.Web.Models;

namespace Montech.Web.Repository.Sessao;

public interface ISessaoInterface
{
    void CriarSessao(UsuarioModel usuarioModel);
    void RemoveSessao();
    UsuarioModel BuscarSessao();
}
