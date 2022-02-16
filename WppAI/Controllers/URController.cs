using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WppAI.Models;
using System.Web.Security;

namespace WppAI.Controllers
{
   
    public class URController : Controller
    {

        EmployeeDetailsEntities1 db = new EmployeeDetailsEntities1();
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string UName, string Password)
        {
            var valid = db.Employees.Where(x => x.UName == UName && x.Password == Password).FirstOrDefault();
            if(valid != null)
            {
                FormsAuthentication.SetAuthCookie(UName,false);
              return  RedirectToAction("BranchEntry","WI"); 
            }
            return View();
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction ("LogIn","UR");
        }
      

    }
}