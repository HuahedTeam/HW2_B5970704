using HW2_B5970704.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using HW2_B5970704.Controllers;

public class OrderDetailsController : Controller
{
    private PhoneEntities db = new PhoneEntities();

    public ActionResult Index()
    {
        var orderDetails = db.order_details.Include(o => o.Phone);
        return View(orderDetails.ToList());
    }

    public ViewResult ViewOrderDetails()
    {
        IEnumerable<OrderDetailsViewModel> model = null;
        model = (from prod in db.Phone
                 join o in db.order_details on prod.product_id equals o.product_id
                 select new OrderDetailsViewModel
                 {
                     Brand = prod.brand,
                     Model = prod.model,
                     Price = (decimal)prod.price,
                     Amount = (int)o.amount,
                     Total = (decimal)o.sub_total
                 });
        return View(model);
    }
    public HttpResponseBase ExcelExport()
    {
        var api = new APIController();
        var jsonDataExport = JsonConvert.SerializeObject(api.OrderbyModel().Data);
        string filename = "OrderSummary";
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(jsonDataExport, (typeof(DataTable)));
        string fullDetail = string.Empty;
        string tab = "";
        foreach (DataColumn dc in dt.Columns)
        {
            fullDetail = fullDetail + tab + dc.ColumnName;
            tab = ",";
        }
        fullDetail = fullDetail + "\n";
        int ii;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (ii = 0; ii < dt.Columns.Count; ii++)
            {
                fullDetail = fullDetail + tab + dr[ii].ToString().Replace(',', '‚');
                tab = ",";
            }
            fullDetail = fullDetail + "\n";
        }
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("contentdisposition", string.Format("attachment;filename={0}.csv", filename));
        Response.ContentType = "text/csv";
        Response.ContentEncoding = Encoding.UTF32;
        byte[] BOX = new byte[] { 0xef, 0xbb, 0xbf };
        Response.BinaryWrite(BOX);
        Response.BinaryWrite(Encoding.UTF8.GetBytes(fullDetail));
        Response.Flush();
        Response.End();
        return null;
    }

}