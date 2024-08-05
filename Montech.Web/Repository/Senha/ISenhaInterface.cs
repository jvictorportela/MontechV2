namespace Montech.Web.Repository.Senha;

public interface ISenhaInterface
{
    void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);

    bool VerificarSenha(string senha, byte[] senhaHash, byte[] senhaSalt);
}
