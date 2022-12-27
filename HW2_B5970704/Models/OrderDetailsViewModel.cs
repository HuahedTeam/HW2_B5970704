using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HW2_B5970704.Models;

namespace HW2_B5970704.Models
{
    public class OrderDetailsViewModel
    {
     public string Brand { get; set; }
     public string Model { get; set; }
     public decimal? Price { get; set; }
     public int? Amount { get; set; }
     public decimal? Total { get; set; }
     }
}