using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HW2_B5970704.Models;

namespace HW2_B5970704.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public JsonResult OrderbyModel()
        {
            var orderModel = new OrderDetailsDataModel();
            var data = orderModel.GetOrderbyModel();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}