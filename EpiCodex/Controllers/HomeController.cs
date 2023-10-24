using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EpiCodex.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;


namespace EpiCodex.Controllers
{
  public class HomeController : Controller
  {
    private readonly EpiCodexContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
      public HomeController(UserManager<ApplicationUser> userManager, EpiCodexContext db)
      {
        _userManager = userManager;
        _db = db;
      }

    
    public async Task<ActionResult> Index()
    {
        string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Dictionary<string, object[]> model = new Dictionary<string, object[]>();
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        if (currentUser != null)
        {
          Project[] projects = _db.Projects
                      .Where(entry => entry.User.Id == currentUser.Id)
                      .ToArray();
          model.Add("projects", projects);
        }
        return View(model);

    }
  }
}