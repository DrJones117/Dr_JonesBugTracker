using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Dr_JonesBugTracker.Models;


public class Project
{
    [Key]
    public int ProjectId { get; set; }

    [Required]
    [MinLength(2)]
    public string Name { get; set; }

    [Required]
    [MinLength(10)]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; }

    public User? Creator { get; set; }
    public List<Bug> AllBugs { get; set; } = new List<Bug>();
}
