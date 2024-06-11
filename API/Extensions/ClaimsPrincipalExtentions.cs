
using System.Security.Claims;

public static class ClaimsPrincipalExtentions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
       return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}