using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EpiCodex.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace EpiCodex.Controllers
{
  [Authorize]
  public class ProjectsController : Controller
  {
    private readonly EpiCodexContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProjectsController(EpiCodexContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;

    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Project> userProjects = _db.Projects
                            .Where(p=>p.User.Id == currentUser.Id)
                            .ToList();
      return View(userProjects);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Project project)
    {
      if(!ModelState.IsValid)
      {
        return View(project);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        project.User = currentUser;
        _db.Projects.Add(project);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Project thisProject = _db.Projects
                          .Include(project => project.JoinEntities)
                          .ThenInclude(join => join.Technology)
                          .FirstOrDefault(project => project.ProjectId == id);
      return View(thisProject);
    }

    public ActionResult Edit(int id)
    {
      Project thisProject = _db.Projects.FirstOrDefault(project => project.ProjectId == id);
      //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisProject);
    }

    [HttpPost]
    public ActionResult Edit(Project project)
    {
      _db.Projects.Update(project);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Project thisProject = _db.Projects.FirstOrDefault(project => project.ProjectId == id);
      return View(thisProject);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Project thisProject = _db.Projects.FirstOrDefault(project => project.ProjectId == id);
      _db.Projects.Remove(thisProject);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
      public ActionResult AddTechnology(int id)
    {
      Project thisProject = _db.Projects.FirstOrDefault(project => project.ProjectId == id);
      ViewBag.TechnologyId = new SelectList(_db.Technologies, "TechnologyId", "Type");
      return View(thisProject);
    }

    [HttpPost]
    public ActionResult AddTechnology(Project project, int technologyId)
    {
      #nullable enable
      ProjectTechnology? joinEntity = _db.ProjectTechnologies.FirstOrDefault(join => (join.TechnologyId == technologyId && join.ProjectId == project.ProjectId));
      #nullable disable
      if(joinEntity == null && technologyId != 0)
      {
        _db.ProjectTechnologies.Add(new ProjectTechnology { TechnologyId = technologyId, ProjectId = project.ProjectId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = project.ProjectId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      ProjectTechnology joinEntry = _db.ProjectTechnologies.FirstOrDefault(entry => entry.ProjectTechnologyId == joinId);
      _db.ProjectTechnologies.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}