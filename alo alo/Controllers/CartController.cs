using alo_alo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace alo_alo.Controllers
{
    public class CartController : Controller
    {
        DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as
            List<CartItem>;
            //Nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào Session
            if (myCart == null)
            {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
            }
            return myCart;
        }

        public ActionResult AddToCart(int id)
        {
            //Lấy giỏ hàng hiện tại
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p =>
            p.ProductID == id);
            if (currentProduct == null)
            {
                currentProduct = new CartItem(id);
                myCart.Add(currentProduct);
            }
            else
            {
                currentProduct.Number++; //Sản phẩm đã có trong giỏ thì tăng số lượng lên 1
            }
            return RedirectToAction("Index", "CustomerProducts", new
            {
                id = id
            });
        }

        private int GetTotalNumber()
        {
            int totalNumber = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalNumber = myCart.Sum(sp => sp.Number);
            return totalNumber;
        }

        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalPrice = myCart.Sum(sp => sp.FinalPrice());
            return totalPrice;
        }

        //Xây dựng hàm hiển thị thông tin bên trong giỏ hàng
        public ActionResult GetCartInfo()
        {
            List<CartItem> myCart = GetCart();
            //Nếu giỏ hàng trống thì trả về trang ban đầu
            if (myCart == null || myCart.Count == 0)
            {
                return RedirectToAction("Index", "CustomerProducts");
            }
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart); //Trả về View hiển thị thông tin giỏhàng
        }











        //Viết hàm cập nhật lại số lượng sản phẩm ở mỗi dòng sản phẩm khi khách hàng muốn đặt mua thêm
        public void Update_quantity(int id, int _new_quan)
        {
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p =>
             p.ProductID == id);

            currentProduct.Number = _new_quan;
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            List<CartItem> myCart = GetCart();
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);

            Update_quantity(id_pro, _quantity);
            return RedirectToAction("GetCartInfo", "Cart");
        }















        //Viết hàm xóa sản phẩm trong giỏ hàng
        public void Remove_CartItem(int id)
        {
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p =>
             p.ProductID == id);

            myCart.Remove(currentProduct);
        }
        //Action Xoá dòng sp trong giỏ hàng
        public ActionResult RemoveCart(int id)
        {
            List<CartItem> myCart = GetCart();
            Remove_CartItem(id);
            
            return RedirectToAction("GetCartInfo", "Cart");
        }





        //Viết hàm xóa giỏ hàng sau khi Khách hàng thực hiện thanh toán
        public void ClearCart()
        {
            List<CartItem> myCart = GetCart();
            myCart.Clear();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                List<CartItem> myCart = GetCart();
                OrderPro _oder = new OrderPro();   // Bảng hoá đơn sp
                _oder.DateOrder = DateTime.Now;
                _oder.AddressDeliverry = form["AddressDeliverry"];
                string ten = form["NameCustomer"];
                var khachhang = db.Customers.FirstOrDefault(k => k.NameCus == ten);
                _oder.IDCus = khachhang.IDCus;
                db.OrderProes.Add(_oder);
                foreach(var item in myCart)
                {
                    OrderDetail _oder_detail = new OrderDetail();
                    _oder_detail.IDOrder = _oder.ID;
                    _oder_detail.IDProduct = item.ProductID;
                    _oder_detail.UnitPrice = (double)item.Price;
                    _oder_detail.Quantity = item.Number;
                    db.OrderDetails.Add(_oder_detail);
                }
                db.SaveChanges();
                ClearCart();
                return RedirectToAction("CheckOut_Success", "Cart");
            }
            catch
            {
                return Content("Có vẻ như bạn chưa đăng ký. Hãy đăng ký và thử lại!");
            }
        }

        public ActionResult CheckOut_Success()
        {
            return View();
        }












        //Hiển thị logo xe đẩy
        public ActionResult CartPartial()
        {
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return PartialView();
        }







    }
}