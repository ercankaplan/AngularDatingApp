
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;
public record LoginDto
{
    [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
    public string Username { get; init; }

    [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
    public string Password { get; init; }

}
