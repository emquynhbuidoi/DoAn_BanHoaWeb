using alo_alo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

using PagedList;
using PagedList.Mvc;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Drawing;


namespace alo_alo.Controllers
{
    public class CustomerProductsController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: CustomerProducts
        public ActionResult Index(int? page, string categories)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);

            var products = db.Products.OrderByDescending(x => x.NamePro);
            return View(products.ToPagedList(pageNum, pageSize));

        }
        public ActionResult Indexmoi(string tensp, string categories)
        {
            if (categories != null)
            {
                var products = db.Products.OrderByDescending(x => x.NamePro).Where(p => p.Category == categories);
                return View(products);
            }
            else
            {
                if (tensp != null && tensp != "")
                {
                    var products = db.Products.OrderByDescending(x => x.NamePro);

                    return View(products.Where(p => p.NamePro == tensp));
                }
                else
                {

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }


        public ActionResult GetProductsByCategory()
        {
            var categories = db.Categories.ToList();
            return PartialView("CategoriesPartialView", categories);
        }

        //public ActionResult GetProductsByCateId(int id)
        //{
        //    var products = db.Products.Where(p => p.Category1.Id == id).OrderByDescending(x => x.NamePro);


        //    return RedirectToAction("Index", "CustomerProducts");
        //}




    }
}