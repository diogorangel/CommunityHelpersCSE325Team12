using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CommunityHelpers.Blazor.Components;
using CommunityHelpers.Blazor.Components.Account;
using CommunityHelpers.Blazor.Data;

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVIÇOS DO BLAZOR ---
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

// --- CONFIGURAÇÃO DO BANCO DE DADOS (VERSÃO ROBUSTA) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 1. Registra a Factory (Para seus componentes Razor)
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// 2. Registra o DbContext Scoped (Para o Identity e EF Tools)
// Ele vai buscar a mesma configuração que a Factory já possui
builder.Services.AddScoped(p => 
    p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// --- 3. CONFIGURAÇÃO DO IDENTITY ---
builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false; // Facilitar o teste local
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// --- 4. PIPELINE DE REQUISIÇÕES (MIDDLEWARE) ---
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// Importante para carregar CSS, JS e Imagens da wwwroot
app.UseStaticFiles();

app.UseAntiforgery();

// Mapeamento dos componentes Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Adiciona os endpoints de Login, Logout e Register
app.MapAdditionalIdentityEndpoints();

app.Run();