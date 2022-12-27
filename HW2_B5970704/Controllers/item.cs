using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW2_B5970704.Models;

namespace HW2_B5970704.Controllers
{
    public class item
    {
        private Phone phone = new Phone();
 public Phone Phone
 {
 get { return phone; }
 set { phone = value; }
 }
 private int quantity;
 public int Quantity
 {
 get { return quantity; }
 set { quantity = value; }
 }
 public item()
{ }
public item(Phone phone, int quantity)
 {
 this.phone = phone;
this.quantity = quantity;
 }
    }
}