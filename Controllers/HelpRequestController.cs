using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommunityHelpers.Web.Data;
using CommunityHelpers.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace CommunityHelpers.Web.Controllers;

[Authorize] // Garante que só usuários logados acessem
public class HelpRequestsController : Controller
{
    private readonly ApplicationDbContext _context;

    public HelpRequestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // LISTAR: Ver todos os pedidos
    public async Task<IActionResult> Index()
    {
        return View(await _context.HelpRequests.ToListAsync());
    }

    // CRIAR: Tela de formulário
    public IActionResult Create()
    {
        return View();
    }

    // CRIAR: Salvar no banco
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,Category")] HelpRequest helpRequest)
    {
        if (ModelState.IsValid)
        {
            helpRequest.RequesterId = User.Identity?.Name ?? "Anônimo";
            helpRequest.Status = "Pending";
            _context.Add(helpRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(helpRequest);
    }

    // MODO CLAIM: O voluntário aceita a tarefa
    [HttpPost]
    public async Task<IActionResult> Claim(int id)
    {
        var request = await _context.HelpRequests.FindAsync(id);
        if (request != null && request.Status == "Pending")
        {
            request.VolunteerId = User.Identity?.Name; 
            request.Status = "In-Progress";
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}