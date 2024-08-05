using Microsoft.EntityFrameworkCore;
using Montech.Web.Data;
using Montech.Web.Repository;
using Montech.Web.Repository.Login;
using Montech.Web.Repository.Produto;
using Montech.Web.Repository.Senha;
using Montech.Web.Repository.ServicoPrestado;
using Montech.Web.Repository.Sessao;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DeveloperConnection")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IProdutoInterface, ProdutoService>();
builder.Services.AddScoped<IServicoPrestadoInterface, ServicoPrestadoService>();
builder.Services.AddScoped<ILoginInterface, LoginServices>();
builder.Services.AddScoped<ISenhaInterface, SenhaServices>();
builder.Services.AddScoped<ISessaoInterface, SessaoService>();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
