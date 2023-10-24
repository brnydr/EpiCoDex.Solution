using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EpiCodex.Models;

public class Technology
{

  public int TechnologyId{ get; set; }
  [Required(ErrorMessage = "Please enter a type for your project.")]
  public string Type { get;  set;}
  public List<ProjectTechnology> JoinEntities { get; set;  }  

}