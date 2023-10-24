
namespace EpiCodex.Models;

public class ProjectTechnology
{
  public int ProjectTechnologyId { get;  set;}
  public Technology Technology{ get; set; }
  public int TechnologyId { get; set; }
  public Project Project { get; set; }
  public int ProjectId{ get; set; }
}