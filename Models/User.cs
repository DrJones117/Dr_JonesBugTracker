using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dr_JonesBugTracker.Models;


public class User
{
    [Key]
    public int UserId {get; set;}

    [Required]
    [EmailAddress]
    public required string Email {get; set;}

    [Required]
    [DataType(DataType.Password)]
    [MinLength(10, ErrorMessage = "Password must be at least 10 characters")]
    public required string Password {get; set;}

    [NotMapped]
    [Compare("Password")]
    public required string PasswordConfirm { get; set; }

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    public List<Project> AllProjects {get; set;} = new List<Project>();
}

public class UniqueEmailAttribute : ValidationAttribute
{ 
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("Email is required!");
        }
    
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        if(_context.Users.Any(e => e.Email == value.ToString()))
        {
            return new ValidationResult("Email must be unique!");
        } else {
            return ValidationResult.Success;
        }
    }
}