using HW2_B5970704.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW2_B5970704.Controllers
{
    public class EmployeesController : Controller
    {
        PhoneAjax _dbContext;
        public EmployeesController()
        {
            _dbContext = new Models.PhoneAjax();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            var tblEmployeess = _dbContext.Employees.ToList();
            return Json(tblEmployeess, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var employee = _dbContext.Employees.ToList().Find(x => x.ID == id);
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
            }
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(employee).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = _dbContext.Employees.ToList().Find(x => x.ID == id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }
            return Json(employee, JsonRequestBehavior.AllowGet);
        }
    }
}
