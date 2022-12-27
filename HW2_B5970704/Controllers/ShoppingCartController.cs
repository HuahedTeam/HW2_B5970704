using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW2_B5970704.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HW2_B5970704.Controllers
{
    public class ShoppingCartController : Controller
    {
        PhoneEntities pe = new PhoneEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        private int isExisting(int id)
        {
            List<item> cart = (List<item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Phone.product_id == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public ActionResult AddtoCart(int id)
        {
            if (Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                cart.Add(new item(pe.Phone.Find(id), 1));
                Session["cart"] = cart;
            }
            else
            {
                List<item> cart = (List<item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                {
                    cart.Add(new item(pe.Phone.Find(id), 1));
                }
                else
                {
                    cart[index].Quantity++;
                }
                Session["cart"] = cart;
            }

            return View("Cart");
        }
        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            List<item> cart = (List<item>)Session["cart"];
            if (cart[index].Quantity > 1)
            {
                cart[index].Quantity--;
            }
            else
            {
                cart.RemoveAt(index);
            }

            Session["cart"] = cart;
            return View("Cart");
        }
        public ActionResult Save_Order(FormCollection fc)
        {
            List<item> items = (List<item>)Session["cart"];

            decimal summary = 0;
            foreach (item it in (List<item>)Session["cart"])
            {
                summary += (it.Phone.price.Value * it.Quantity);
            }

            orders orders = new orders();
            orders.customer_id = User.Identity.GetUserId();
            orders.grand_total = summary;
            orders.card_no = fc["credit_card"];
            orders.order_date = DateTime.Now;
            orders.order_status = "New";
            pe.orders.Add(orders);
            pe.SaveChanges();
            foreach (item it in items)
            {
                order_details od = new order_details();
                od.order_id = pe.orders.Max(item => item.order_id);
                od.product_id = it.Phone.product_id;
                od.amount = it.Quantity;
                od.sub_total = (it.Phone.price * it.Quantity);
                pe.order_details.Add(od);
                pe.SaveChanges();
            }

            return View("Confirm");
        }
    }
}