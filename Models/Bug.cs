using System.ComponentModel.DataAnnotations;
namespace Dr_JonesBugTracker.Models;

public class Bug
{
    [Key]
    public int BugId {get; set;}

    [Required]
    [MinLength(2)]
    public required string Name {get; set;}

    [Required]
    [MinLength(5)]
    public required string Status {get; set;}

    [Required]
    [MinLength(10)]
    public required string Description {get; set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public int ProjectId {get; set;}

    public Project? BugHost {get; set;}
}