using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Criminal_RM_Using_MVC.Models;

namespace Criminal_RM_Using_MVC.Controllers
{
    public class SpecialActionsController : Controller
    {
        CrimalProjectEntities db = new CrimalProjectEntities();
        // GET: SpecialActions
        public ActionResult Index(int id)
        {
            Victim victim = db.Victims.FirstOrDefault(v=>v.Victim_ID == id);
            var criminals = db.Criminals.Where(c => c.Victim_id == id);
            ViewBag.Victim = victim;
            return View(criminals.ToList());
        }
    }
}