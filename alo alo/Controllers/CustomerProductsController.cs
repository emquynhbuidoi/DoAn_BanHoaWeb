using alo_alo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace alo_alo.Controllers
{
    public class CustomerProductsController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: CustomerProducts
        public ActionResult Index()
        {
            var products = db.Products;
            return View(products.ToList());
        }

        public ActionResult Details(int id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if(pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }














    }
}