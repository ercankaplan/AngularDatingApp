
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;
public class RegisterDto
{
  
  [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
    public string Password { get; set; }
}