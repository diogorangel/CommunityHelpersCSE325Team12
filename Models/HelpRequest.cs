using System.ComponentModel.DataAnnotations;

namespace CommunityHelpers.Web.Models;

public class HelpRequest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;
    
    public string Category { get; set; } = "General"; // Default category
    
    public string Status { get; set; } = "Pending"; 
    
    public string RequesterId { get; set; } = string.Empty;
    public string? VolunteerId { get; set; }
}