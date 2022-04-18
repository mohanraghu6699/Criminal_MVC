using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Criminal_RM_Using_MVC.Models;

namespace Criminal_RM_Using_MVC.Controllers
{
    public class VictimsController : Controller
    {
        private CrimalProjectEntities db = new CrimalProjectEntities();

        [Authorize(Roles = "ACP,JCP,DCP,SP,CP,CI")]
        public RedirectToRouteResult RoutetoVictimData(int id)
        {
            return RedirectToAction("Index", "SpecialActions", new {ID = id});
        }

        // GET: Victims
        [Authorize(Roles = "ACP,JCP,DCP,SP,CP,CI,PC,SI")]
        public ActionResult Index()
        {
            return View(db.Victims.ToList());
        }
        [HttpPost]
        public ActionResult Index(string SearchFilter)
        {
            var victims = db.Victims.Where(v=>v.Victim_Name.Contains(SearchFilter));
            if (victims.ToList().Count > 0)
                return View(victims.ToList());
            return RedirectToAction("NoRecordsFound", "Errors");
        }

        [Authorize(Roles = "ACP,JCP,DCP,SP,CP,CI,PC,SI")]
        // GET: Victims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Victim victim = db.Victims.Find(id);
            if (victim == null)
            {
                return HttpNotFound();
            }
            return View(victim);
        }
        [Authorize(Roles = "ACP,JCP,DCP,SP,CP,CI")]
        // GET: Victims/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Victims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Victim victim)
        {
            if (ModelState.IsValid)
            {
                string Filename = System.IO.Path.GetFileNameWithoutExtension(victim.ImageFile.FileName);
                string Extension = System.IO.Path.GetExtension(victim.ImageFile.FileName);
                Filename = victim.Victim_Name + Extension;
                victim.Image = "../Images/Victims/" + Filename;
                Filename = System.IO.Path.Combine(Server.MapPath("../Images/Victims/"), Filename);
                victim.ImageFile.SaveAs(Filename);
                db.Victims.Add(victim);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }

            return View(victim);
        }
        // GET: Victims/Edit/5
        [Authorize(Roles = "ACP,JCP,DCP,SP,CP,CI,SI")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Victim victim = db.Victims.Find(id);
            if (victim == null)
            {
                return HttpNotFound();
            }
            return View(victim);
        }

        // POST: Victims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Victim victim)
        {
            if (ModelState.IsValid)
            {
                Victim changed = db.Victims.Find(victim.Victim_ID);
                changed.Victim_Name = victim.Victim_Name;
                changed.Gender = victim.Gender;
                if (victim.DOB != null) {
                    changed.DOB = victim.DOB;
                }
                if (victim.DODeath != null)
                {
                    changed.DODeath = victim.DODeath;
                }
                changed.Cause_Of_Death = victim.Cause_Of_Death;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(victim);
        }
        [Authorize(Roles = "ACP,JCP,DCP,SP,CP,CI")]
        // GET: Victims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Victim victim = db.Victims.Find(id);
            if (victim == null)
            {
                return HttpNotFound();
            }
            return View(victim);
        }

        // POST: Victims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Victim victim = db.Victims.Find(id);
            db.Victims.Remove(victim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
