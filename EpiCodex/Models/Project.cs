using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EpiCodex.Models;

public class Project
{
  public int ProjectId{ get; set; }

  [Required(ErrorMessage = "Please enter a title for your project.")]
  public string Title { get;  set;}

  public string Description{ get; set; }

  public ApplicationUser User{ get; set; }

  public List<ProjectTechnology> JoinEntities { get; set; }
}