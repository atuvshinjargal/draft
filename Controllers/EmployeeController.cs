using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using asp.NETMVCCRUD.Models;

namespace asp.NETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using(mvcCRUDDBEntities db = new mvcCRUDDBEntities())
            {
                List<employee> emplist = db.employees.ToList<employee>();
                return Json(new { data = emplist }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(employee newEmployee)
        {

            if (ModelState.IsValid)
            {
                using (mvcCRUDDBEntities db = new mvcCRUDDBEntities())
                {
                    db.employees.Add(newEmployee);
                    db.SaveChanges();
                }
                   

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int? id)
        {
            employee t;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (mvcCRUDDBEntities db = new mvcCRUDDBEntities())
            {
                 t = db.employees.Find(id);
            }
            if (t == null)
            {
                return HttpNotFound();
            }
            return View(t);
        }
        [HttpPost]
        public ActionResult Edit(employee newEmployee)
        {

            if (ModelState.IsValid)
            {
                using (mvcCRUDDBEntities db = new mvcCRUDDBEntities())
                {
                    db.Entry(newEmployee).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Delete(int? id)
        {

            using (mvcCRUDDBEntities db = new mvcCRUDDBEntities())
            {
                employee em = db.employees.Where(x => x.EMPLOYEEID == id).FirstOrDefault<employee>();
                db.employees.Remove(em);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}