using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CocodriloStore.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurações de conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var env = builder.Environment;

    if (env.IsDevelopment())
    {
        // SQLite para desenvolvimento
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else
    {
        // SQL Server para produção
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

// Configuração do Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<AppDbContext>();

// MVC Controllers com Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Autenticação
app.UseAuthorization();  // Autorização

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Habilita páginas de login/registro do Identity

app.Run();