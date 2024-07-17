namespace Montech.Web.Models;

public class PaginacaoModel
{
    public int PaginaAtual { get; set; }
    public int TotalItens { get; set; }
    public int ItensPorPagina { get; set; }

    public int TotalPaginas => (int)Math.Ceiling((double)TotalItens / ItensPorPagina);
}
