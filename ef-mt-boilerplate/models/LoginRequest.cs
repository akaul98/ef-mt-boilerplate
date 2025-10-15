using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }
      // Add Password if you want to check credentials
      // public string Password { get; set; }
  }