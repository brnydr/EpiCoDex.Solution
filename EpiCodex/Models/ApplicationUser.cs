using Microsoft.AspNetCore.Identity;
using System;

namespace EpiCodex.Models;

public class ApplicationUser : IdentityUser
{
  public string DisplayName{get; set;}
  public string Bio {get; set;}
}