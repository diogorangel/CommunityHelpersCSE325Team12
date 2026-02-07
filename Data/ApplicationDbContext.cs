using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CommunityHelpers.Web.Models; // Adicione esta linha para ele reconhecer o modelo

namespace CommunityHelpers.Web.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext(options)
{
    // Esta linha diz ao .NET: "Crie uma tabela chamada HelpRequests baseada na classe HelpRequest"
    public DbSet<HelpRequest> HelpRequests { get; set; }
}