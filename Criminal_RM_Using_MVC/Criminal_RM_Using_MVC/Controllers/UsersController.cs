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
    public class UsersController : Controller
    {
        private CrimalProjectEntities db = new CrimalProjectEntities();
        public JsonResult IsUserNameValid(string User_Name)
        {
            return Json(!db.Users.Any(user => user.User_Name == User_Name), JsonRequestBehavior.AllowGet);
        }
        // GET: Users
        [Authorize(Roles = "ACP,JCP,DCP")]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Position);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "ACP,JCP,DCP")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "ACP,JCP,DCP")]
        public ActionResult Create()
        {
            ViewBag.Position_id = new SelectList(db.Positions, "Position_ID", "Position_Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_ID,User_Name,User_Password,Position_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Position_id = new SelectList(db.Positions, "Position_ID", "Position_Name", user.Position_id);
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "ACP,JCP,DCP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Position_id = new SelectList(db.Positions, "Position_ID", "Position_Name", user.Position_id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_ID,User_Name,User_Password,Position_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Position_id = new SelectList(db.Positions, "Position_ID", "Position_Name", user.Position_id);
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "ACP,JCP,DCP")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
