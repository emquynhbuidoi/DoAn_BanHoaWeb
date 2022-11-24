using alo_alo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace alo_alo.Controllers
{
    public class OrderDetailsController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: OrderDetails
        public ActionResult Index(int id)
        {
            var oderdetails = db.OrderDetails.Where(s => s.IDOrder == id);
            ViewBag.TotalNumber = GetTotalNumber(id);
            ViewBag.TotalPrice = TongTien(id);

            return View(oderdetails);
        }



        public decimal TongTien(int id)
        {
            decimal gia = 0;
            var oderdetails = db.OrderDetails.Where(s => s.IDOrder == id);
            foreach(var ne in oderdetails)
            {
                gia += (decimal)(ne.Quantity * ne.UnitPrice);
            }
            return gia;
        }



        private int GetTotalNumber(int id)
        {
            int totalNumber = 0;
            var oderdetails = db.OrderDetails.Where(s => s.IDOrder == id);
            totalNumber = (int)oderdetails.Sum(sp => sp.Quantity);
            return totalNumber;
        }





    }
}