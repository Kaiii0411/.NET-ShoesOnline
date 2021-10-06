using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoesOnline.Models;

namespace ShoesOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private ShoesOnlineEntities db = new ShoesOnlineEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.maQuanHuyen = new SelectList(db.quanHuyens.ToList().OrderBy(n => n.maQH),"maQH","tenQH");
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, taiKhoanTV tk)
        {
            ViewBag.maQuanHuyen = new SelectList(db.quanHuyens.ToList().OrderBy(n => n.maQH),"maQH","tenQH");
            var firstname = collection["FirstName"];
            var lastname = collection["LastName"];
            var gender = collection["Gender"];
            var address = collection["Address"];
            var district = Convert.ToInt32(collection["maQuanHuyen"]);
            var birthday = String.Format("{0:dd/MM/yyyy}", collection["Birthday"]);
            var phone = collection["Phone"];
            var email = collection["Email"];
            var user = collection["User"];
            var password = collection["Password"];
            var confirmpass = collection["ConfirmPass"];
            if (String.IsNullOrEmpty(firstname))
            {
                ViewData["Error1"] = "First name is a required field";
            }
            if (String.IsNullOrEmpty(lastname))
            {
                ViewData["Error2"] = "Last name is a required field";
            }
            else if (String.IsNullOrEmpty(phone))
            {
                ViewData["Error3"] = "Please enter a valid phone number";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Error4"] = "Please enter a valid email";
            }
            else if (String.IsNullOrEmpty(user))
            {
                ViewData["Error5"] = "User name is a required field";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Error6"] = "Password is required";
            }
            else if (String.IsNullOrEmpty(confirmpass))
            {
                ViewData["Error7"] = "Confirm password is required";
            }
            else
            {
                tk.tenTV = firstname;
                tk.hoDem = lastname;
                tk.gioiTinh = gender;
                tk.diaChi = address;
                tk.maQH = district;
                tk.ngaySinh = DateTime.Parse(birthday);
                tk.soDT = phone;
                tk.email = email;
                tk.taiKhoanKH = user;
                tk.matKhau = password;
                db.taiKhoanTVs.Add(tk);
                int res = db.SaveChanges();
                if (res > 0)
                {
                    KhachHang kh = new KhachHang();
                    kh.tenKH = firstname;
                    kh.gioiTinh = gender;
                    kh.diaChi = address;
                    kh.maQH = district;
                    kh.ngaySinh = DateTime.Parse(birthday);
                    kh.soDT = phone;
                    kh.email = email;
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                    Response.Write("Submited Successfully");
                }
                else
                {

                    Response.Write("Try Again!!!");

                }
                ModelState.Clear();

                return RedirectToAction("Login");
            }
            return this.Register();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var user = collection["User"];
            var password = collection["Password"];
            if (String.IsNullOrEmpty(user))
            {
                ViewData["Error1"] = "User is a required field";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Error2"] = "Password is required";
            }
            else
            {
                taiKhoanTV tk = db.taiKhoanTVs.SingleOrDefault(m => m.taiKhoanKH == user && m.matKhau == password);
                if (tk != null)
                {
                    Session["User"] = tk;
                    Session["Name"] = tk.taiKhoanKH;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Notification = "Username or password is incorrect!";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "User");
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult ChangePassword(taiKhoanTV obj)
        {
            if (obj.newpass == obj.confirmpass)
            {
                var data = db.taiKhoanTVs.Where(m => m.email == obj.email).FirstOrDefault();
                data.matKhau = obj.confirmpass;
                db.SaveChanges();
            }
            else
            {
                ViewBag.data = "password doesn't match";
            }
            Session.Clear();
            return RedirectToAction("Logout", "User");
        }
    }
}