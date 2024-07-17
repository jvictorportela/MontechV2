using Microsoft.EntityFrameworkCore;
using Montech.Web.Models;

namespace Montech.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ProdutoModel> Produtos { get; set; }
    public DbSet<EmpresaModel> Empresas { get; set; }
    public DbSet<CategoriaModel> Categorias { get; set; }
    public DbSet<UsuarioModel> Usuarios { get; set; }
}
