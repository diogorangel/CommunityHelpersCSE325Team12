using Microsoft.AspNetCore.Identity;

namespace CommunityHelpers.Blazor.Data;

public class ApplicationUser : IdentityUser
{
    // Define se o usuário é primariamente um ajudante ou quem pede ajuda
    public string UserType { get; set; } = "Requester"; // Volunteer ou Requester
}