using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CommunityHelpers.Blazor.Components;
using CommunityHelpers.Blazor.Components.Account;
using CommunityHelpers.Blazor.Data;

var builder = WebApplication.CreateBuilder(args);

// --- 1. BLAZOR SERVICES ---
// Adds support for Razor Components and enables Interactive Server Side Rendering (SSR)
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

// --- 2. DATABASE CONFIGURATION (ROBUST VERSION) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register the DbContext Factory (Required for Blazor Server components using IDbContextFactory)
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Register the Scoped DbContext (Required for Identity and EF Migration Tools)
// It retrieves the same configuration defined in the Factory above
builder.Services.AddScoped(p => 
    p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// --- 3. IDENTITY CONFIGURATION ---
builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false; // Set to false to simplify local testing
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

// --- 4. REQUEST PIPELINE (MIDDLEWARE) ---
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

// Essential for loading CSS, JS, and user-uploaded images from wwwroot/uploads
app.UseStaticFiles();

// Protects forms against Cross-Site Request Forgery (required for EditForm)
app.UseAntiforgery();

// Map Blazor components and enable Interactive Server Render Mode globally

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Adds Identity endpoints for Login, Logout, and Register
app.MapAdditionalIdentityEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
app.Run();