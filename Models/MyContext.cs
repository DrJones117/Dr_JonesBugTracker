#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace Dr_JonesBugTracker.Models;
public class MyContext : DbContext 
{
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<User> Users { get; set; } 
    public DbSet<Project> Projects { get; set; } 
    public DbSet<Bug> Bugs { get; set; } 
}
