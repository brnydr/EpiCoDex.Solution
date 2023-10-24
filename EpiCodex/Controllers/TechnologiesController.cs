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

namespace EpiCodex.Controllers;
public class TechnologiesController: Controller
{
  private readonly EpiCodexContext _db;
  public TechnologiesController(EpiCodexContext db)
  {
    _db = db;
  }
  public ActionResult Index()
  {
    return View(_db.Technologies.ToList());
  }

  public ActionResult Details(int id)
  {
    Technology thisTech = _db.Technologies.Include(technology => technology.JoinEntities).ThenInclude(join => join.Project)
                  .FirstOrDefault(tech => tech.TechnologyId == id);
    return View(thisTech);    
  }
  public ActionResult Create()
  {
    return View();
  }
  
  [HttpPost]
  public ActionResult Create(Technology tech)
  {
    _db.Technologies.Add(tech);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    Technology thisTech = _db.Technologies.FirstOrDefault(tech => tech.TechnologyId == id);
    return View(thisTech);
  }

  [HttpPost]
  public ActionResult Edit(Technology tech)
  {
    _db.Technologies.Update(tech);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Delete(int id)
  {
    Technology tech = _db.Technologies.FirstOrDefault(tech => tech.TechnologyId == id);
    return View(tech);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Technology tech = _db.Technologies.FirstOrDefault(tech => tech.TechnologyId == id);
    _db.Tags.Remove(tech);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  [HttpPost]
  public ActionResult DeleteJoin(int joinId)
  {
    ProjectTechnology joinEntry = _db.ProjectTechnologies.FirstOrDefault(entry => entry.ProjectTechnologyId == joinId);
    _db.ItemTags.Remove(joinEntry);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

}