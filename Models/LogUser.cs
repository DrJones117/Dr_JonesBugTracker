#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.JSInterop.Infrastructure;

public class LoginUser
{
    [Required]    
    public required string LogEmail { get; set; }
    [Required]    
    public required string LogPassword { get; set; }
}
