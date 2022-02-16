using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WppAI.Models;
using System.IO;
namespace WppAI.Controllers
{
    [Authorize]
    
    public class WIController : Controller

    {
        EmployeeDetailsEntities1 db = new EmployeeDetailsEntities1();
        #region EmployeeInfo
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EmployeeInfo(int? id)

        {
            //Employee getid = db.Employees.Find(id);
            Employee getid = db.Employees.Where(x=>x.EmployeeId==id).FirstOrDefault();
            if (getid == null)
            {
                getid  = new Employee();
            }
            return View(getid);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EmployeeInfo(Employee emp,HttpPostedFileBase Photo)
        {
            var Update = db.Employees.Where(x => x.EmployeeId == emp.EmployeeId).FirstOrDefault();
            if (Update == null)
            {
                string F=Path.GetFileNameWithoutExtension(Photo.FileName);
                string E = Path.GetExtension(Photo.FileName);
                string PhotoName = F + DateTime.Now.ToString("yymmssff")+E;
                emp.Photo=PhotoName;
                Photo.SaveAs(Path.Combine(Server.MapPath("~/APPfile"),PhotoName));
                db.Employees.Add(emp);
                db.SaveChanges();
            }
            else
            {
                Update.EmployeeId = emp.EmployeeId;
                Update.EMPName = emp.EMPName;
                Update.Salary = emp.Salary;
                Update.Age = emp.Age;
                Update.DesignationId = emp.DesignationId;
                Update.BranchId = emp.BranchId;
                Update.GenderId = emp.GenderId;
                Update.Role = emp.Role;
                db.SaveChanges();
            }

            return View(emp);

        }
        [Authorize(Roles = "admin")]
        public ActionResult delEmployeeInfo(int? id)

        {
            //Employee getid = db.Employees.Find(id);
            Employee getid = db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (getid == null)
            {
                getid = new Employee();
            }
            string PhotoName = getid.Photo;
            db.Employees.Remove(getid);
            db.SaveChanges();
            if(PhotoName!= null)
            {
                FileInfo fi = new FileInfo(Path.Combine(Server.MapPath("~/APPfile"),PhotoName));
                fi.Delete(); 
            }
         return  RedirectToAction("EmployeeInfo");
        }
        #endregion EmployeeInfo
        #region BranchInfo
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult BranchEntry(int? id)
        {
            Branch getid = db.Branches.Where(x => x.BranchId == id).FirstOrDefault();
            if (getid == null)
            {
                getid = new Branch();
            }
            //Branch Br = new Branch();
            return View(getid);
        }
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult BranchEntry(Branch Bra)
        {
            var Update = db.Branches.Where(x => x.BranchId == Bra.BranchId).FirstOrDefault();
            if (Update == null)
            {
                db.Branches.Add(Bra);
                db.SaveChanges();
            }
            else
            {
                Update.BranchId = Bra.BranchId;
                Update.BranchName = Bra.BranchName;
                db.SaveChanges();
            }
            return View(Bra);
        }
        [Authorize(Roles = "admin, user")]
        public ActionResult delBranchEntry(int? id)

        {
           
            Branch getid = db.Branches.Where(x =>x.BranchId == id).FirstOrDefault();
            if (getid == null)
            {
                getid = new Branch();
            }
            db.Branches.Remove(getid);
            db.SaveChanges();
            return RedirectToAction("BranchEntry");
        }
        #endregion BranchInfo
        #region DesignationInfo
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult DesignationInfo(int? id)
        {
            Designation getid = db.Designations.Where(x => x.DesignationId == id).FirstOrDefault();
            if(getid==null)
            {
                getid = new Designation();
            }
            
            return View(getid);
        }
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult DesignationInfo(Designation Des)
        {
            var Update = db.Designations.Where(x => x.DesignationId == Des.DesignationId).FirstOrDefault();
            if(Update==null)
            {
                db.Designations.Add(Des);
                db.SaveChanges();
            }

            else
            {
                Update.DesignationId = Des.DesignationId;
                Update.DesignationName = Des.DesignationName;
                db.SaveChanges();
            }
            return View(Des);
        }
        [Authorize(Roles = "admin, user")]
        public ActionResult delDesignationInfo(int? id)

        {

            Designation getid = db.Designations.Where(x => x.DesignationId == id).FirstOrDefault();
            if (getid == null)
            {
                getid = new Designation();
            }
            db.Designations.Remove(getid);
            db.SaveChanges();
            return RedirectToAction("DesignationInfo");
        }
        #endregion Designation
            #region GenderInfo
        [HttpGet]
        public ActionResult GenderInfo(int? id)
        {
            GenderInfo getid = db.GenderInfoes.Where(x => x.GenderId == id).FirstOrDefault();
            if (getid == null)
            {
                getid = new GenderInfo();
            }
            //GenderInfo Ge = new GenderInfo();
            return View(getid);
        
        }
        [HttpPost]
        public ActionResult GenderInfo(GenderInfo Gen)
        {
            var Update = db.GenderInfoes.Where(x => x.GenderId == Gen.GenderId).FirstOrDefault();
            if(Update==null)
            {
                db.GenderInfoes.Add(Gen);
                db.SaveChanges();
            }
            else
            {
                Update.GenderId = Gen.GenderId;
                Update.GenderName = Gen.GenderName;
                db.SaveChanges();
            }
            return View(Gen);
        }
        public ActionResult delGenderInfo(int? id)

        {

            GenderInfo getid = db.GenderInfoes.Where(x => x.GenderId == id).FirstOrDefault();
            if (getid == null)
            {
                getid = new GenderInfo();
            }
            db.GenderInfoes.Remove(getid);
            db.SaveChanges();
            return RedirectToAction("GenderInfo");
        }
        #endregion GenderInfo
        [HttpGet]
        public ActionResult EmployeeSearch()
        {
            return View();
        }
        public JsonResult GetEmployeeAPI(int? BId,int?GId,int? DId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var EmployeeList = db.Employees.Where(x => x.BranchId == BId && x.GenderId == GId && x.DesignationId == DId).ToList();
            return Json(EmployeeList, JsonRequestBehavior.AllowGet);


        }
      

        
    }
}