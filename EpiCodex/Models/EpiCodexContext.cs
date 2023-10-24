using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EpiCodex.Models
{
  public class EpiCodexContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Project> Projects { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
    public EpiCodexContext(DbContextOptions options) : base(options) { }
  }
}