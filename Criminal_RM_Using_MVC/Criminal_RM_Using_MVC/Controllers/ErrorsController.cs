using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Criminal_RM_Using_MVC.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult NoRecordsFound()
        {
            return View();
        }
    }
}