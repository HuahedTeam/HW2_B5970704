using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW2_B5970704.Models;

namespace HW2_B5970704.Controllers
{
    public class PhoneController : Controller
    {
        PhoneEntities pe = new PhoneEntities();
        // GET: Phone
        public ActionResult Index()
        {
            ViewBag.listProduct = pe.Phone.ToList();
            return View();
        }
    }
}