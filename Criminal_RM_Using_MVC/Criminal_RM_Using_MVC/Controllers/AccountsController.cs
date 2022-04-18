using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Criminal_RM_Using_MVC.Models;
namespace Criminal_RM_Using_MVC.Controllers
{
    public class AccountsController : Controller
    {
        CrimalProjectEntities entities = new CrimalProjectEntities();
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
           
            bool IsValidUser = entities.Users.Any(u => u.User_Name.ToLower() == model.User_Name.ToLower() &&
            u.User_Password == model.User_Password);
            if (IsValidUser)
            {
                FormsAuthentication.SetAuthCookie(model.User_Name, false);
                return RedirectToAction("Index", "Victims");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
         
        }
        public ActionResult Signup()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var users = entities.Positions.ToList();

            foreach (var user in users)
            {
                items.Add(new SelectListItem 
                { 
                    Text = user.Position_Name,
                    Value = user.Position_ID.ToString()
                });
            }
            ViewBag.Position_id = items;
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            entities.Users.Add(model);
            entities.SaveChanges();

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}