using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CommunityHelpers.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração da Conexão com SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)); // Configurado para SQLite

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2. Configuração de Identidade (Login/Senha)
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    {
        options.SignIn.RequireConfirmedAccount = false; // Mudei para false para facilitar seus testes iniciais
        options.Password.RequireDigit = false;          // Torna a senha de teste mais simples
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. Pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Importante para carregar CSS/JS

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();