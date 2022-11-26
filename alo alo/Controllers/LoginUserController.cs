using alo_alo.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace alo_alo.Controllers
{
    public class LoginUserController : Controller
    {
        DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: LoginUser
        public ActionResult LoginAcount()
        {
            Session["NameUser"] = "";
            Session["RoleUser"] = "";

            return View();
        }

        [HttpPost]
        public ActionResult LoginAcount(AdminUser _user)
        {
            //Giải mã mật khẩu
            var user = db.AdminUsers.Where(s => s.NameUser == _user.NameUser).FirstOrDefault();
            bool test = BCrypt.Net.BCrypt.Verify(_user.PasswordUser, user.PasswordUser);



            var check = db.AdminUsers.Where(s => s.NameUser == _user.NameUser && test == true ).FirstOrDefault();

            if (check == null) // login sai thong tin
            {
                if(_user.NameUser != null && _user.PasswordUser != null)
                {
                    ViewBag.ErrorInfo = "Sai thông tin đăng nhập hoặc tài khoản chưa đăng ký";
                }

                return View("LoginAcount");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
              
                Session["RoleUser"] = check.RoleUser;
                Session["NameUser"] = check.NameUser;

                if (check.RoleUser == "Admin")
                {
                    return RedirectToAction("Index", "Products");
                }


                return RedirectToAction("Index", "CustomerProducts");
            }
        }



        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = db.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                if(check_ID == null)  // chua co ID
                {
                    //Mã hoá mật khẩu
                    int costParamerter = 12;
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(_user.PasswordUser, costParamerter);

                    _user.PasswordUser = hashedPassword;

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.AdminUsers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("LoginAcount");
                }
                else
                {
                    ViewBag.ErrorRegister = "ID đã có người sử dụng !";
                    return View();
                }
            }
            return View();
        }





        public ActionResult LogoutAcount()
        {

            return RedirectToAction("LoginAcount");
        }







    }
}