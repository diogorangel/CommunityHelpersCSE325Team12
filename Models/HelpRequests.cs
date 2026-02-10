using System.ComponentModel.DataAnnotations;

namespace CommunityHelpers.Blazor.Models;

public class HelpRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please provide a title.")]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please provide a description of what you need.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a category.")]
    public string Category { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    // The user who created the request
    public string RequesterId { get; set; } = string.Empty;

    // The user who volunteered (optional until claimed)
    public string? VolunteerId { get; set; }
    
    // Path to the uploaded image file
    public string? AttachmentPath { get; set; } 
}