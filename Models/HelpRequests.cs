using System.ComponentModel.DataAnnotations;
using CommunityHelpers.Blazor.Models;
namespace CommunityHelpers.Blazor.Models;

public class HelpRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string RequesterId { get; set; } = string.Empty;
    public string? VolunteerId { get; set; }
    
    // NOVO CAMPO PARA O ANEXO
    public string? AttachmentPath { get; set; } 
}