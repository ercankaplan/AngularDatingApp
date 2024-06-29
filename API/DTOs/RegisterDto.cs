
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;
public class RegisterDto
{

  [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
  public string Username { get; set; }

  [Required]
  public string KnownAs { get; set; }

  [Required]
  public string Gender { get; set; }

  [Required]
  public DateOnly? DateOfBirth { get; set; }// optional to make it required work!


  [Required]
  public string City { get; set; }

  [Required]
  public string Country { get; set; }


  [StringLength(8, MinimumLength = 4)]
  [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
  public string Password { get; set; }

}