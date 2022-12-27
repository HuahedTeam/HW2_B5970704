using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HW2_B5970704.Models;

namespace HW2_B5970704.Models
{
    public class ChartModel
    {
        public string Model { get; set; }
        public int Amount { get; set; }
    }

    public class OrderDetailsDataModel
    {

        private PhoneEntities db = new PhoneEntities();

        public List<ChartModel> GetOrderbyModel()
        {
            var chartDataList = new List<ChartModel>();

            var prod = db.order_details.OrderBy(i => i.product_id).ToList();
            foreach (var item in prod.GroupBy(i => i.product_id))
            {
                var chartData = new ChartModel();
                chartData.Model = item.FirstOrDefault().Phone.model;
                chartData.Amount = item.Count();
                chartDataList.Add(chartData);
            }
            return chartDataList;
        }
    }
}