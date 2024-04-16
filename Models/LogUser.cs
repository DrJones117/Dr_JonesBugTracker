#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

public class LoginUser
{
    [Required]    
    public required string Email { get; set; }    
    [Required]    
    public required string Password { get; set; } 
}
