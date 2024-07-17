using Montech.Web.Enums;

namespace Montech.Web.Models;

public class UsuarioModel
{
    public long Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public EStatus Status { get; set; } = EStatus.Ativo;
}
