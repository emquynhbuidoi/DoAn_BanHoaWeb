using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using alo_alo.Models;

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
            foreach (var ne in oderdetails)
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



        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.IDOrder = new SelectList(db.OrderProes, "ID", "AddressDeliverry");
            ViewBag.IDProduct = new SelectList(db.Products, "ProductID", "NamePro");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDProduct,IDOrder,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDOrder = new SelectList(db.OrderProes, "ID", "AddressDeliverry", orderDetail.IDOrder);
            ViewBag.IDProduct = new SelectList(db.Products, "ProductID", "NamePro", orderDetail.IDProduct);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDOrder = new SelectList(db.OrderProes, "ID", "AddressDeliverry", orderDetail.IDOrder);
            ViewBag.IDProduct = new SelectList(db.Products, "ProductID", "NamePro", orderDetail.IDProduct);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDProduct,IDOrder,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDOrder = new SelectList(db.OrderProes, "ID", "AddressDeliverry", orderDetail.IDOrder);
            ViewBag.IDProduct = new SelectList(db.Products, "ProductID", "NamePro", orderDetail.IDProduct);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderDetail orderDetail = db.OrderDetails.Where(x => x.ID == id).FirstOrDefault();
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sp = db.OrderDetails.Where(x => x.ID == id).FirstOrDefault();
            db.OrderDetails.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
