using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoesOnline.Models;
using ShoesOnline.ViewModels;

using System.IO;
using System.Web.Security;
using WebMatrix.WebData;

namespace ShoesOnline.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        // GET: Admin
        private ShoesOnlineEntities db = new ShoesOnlineEntities();
        public ActionResult Index()
        {
            DashboardViewModel dashboard = new DashboardViewModel();
            dashboard.products_count = db.sanPhams.Count();
            dashboard.orders_count = db.donHangs.Count();
            dashboard.users_count = db.taiKhoanTVs.Count();
            return View(dashboard);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var user = collection["AdUsername"];
            var pass = collection["AdPassword"];
            if (String.IsNullOrEmpty(user))
            {
                ViewData["Error1"] = "Username must be entered!";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["Error2"] = "Password must be entered!";
            }
            else
            {
                QuanTriVien ad = db.QuanTriViens.FirstOrDefault(m => m.taiKhoanQTV == user && m.matKhauQTV == pass);
                if (ad != null)
                {
                    Session["AdminAccount"] = ad.taiKhoanQTV;
                    Session["AdminPass"] = ad.matKhauQTV;
                    Session["AdminName"] = ad.HoTen;
                    Session["AdminEmail"] = ad.email;
                    Session["AdminPhone"] = ad.soDT;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Notification = "Username or Password is incorrect!";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }
        [HttpGet]
        public ActionResult RecoverPasswordAdmin()
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult RecoverPasswordAdmin(QuanTriVien obj)
        {
            if (obj.newpass == obj.confirmpass)
            {
                var data = db.QuanTriViens.Where(m => m.email == obj.email).FirstOrDefault();
                data.matKhauQTV = obj.confirmpass;
                db.SaveChanges();
            }
            else 
            {
                ViewBag.data = "password doesn't match";
            }
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult Products()
        {

            return View(db.sanPhams.ToList());
        }
        [HttpGet]
        public ActionResult AddNewProducts()
        {
            ViewBag.maLoai = new SelectList(db.loaiSPs.ToList().OrderBy(n => n.loaiHang), "maLoai", "loaiHang");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewProducts(sanPham sP, HttpPostedFileBase fileupload)
        {
            ViewBag.maLoai = new SelectList(db.loaiSPs.ToList().OrderBy(n => n.loaiHang), "maLoai", "loaiHang");
            if (fileupload == null)
            {
                ViewBag.Notification = "Vui lòng chọn ảnh sản phẩm";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("/Asset/products/"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Notification = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sP.hinhDD = ("/Asset/products/" + fileName);
                    db.sanPhams.Add(sP);
                    db.SaveChanges();
                }
                return RedirectToAction("Products");
            }
        }
        public ActionResult ProductDetails(string id)
        {
            sanPham sP = db.sanPhams.SingleOrDefault(n => n.maSP == id);
            ViewBag.maSP = sP.maSP;
            if (sP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sP);
        }
        [HttpGet]
        public ActionResult DeleteProduct(string id)
        {
            sanPham sP = db.sanPhams.SingleOrDefault(n => n.maSP == id);
            ViewBag.maSP = sP.maSP;
            if (sP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sP);
        }
        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult ConfirmDelete(String id)
        {
            sanPham sP = db.sanPhams.SingleOrDefault(n => n.maSP == id);
            ViewBag.maSP = sP.maSP;
            if (sP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.sanPhams.Remove(sP);
            db.SaveChanges();
            return RedirectToAction("Products");
        }
        [HttpGet]
        public ActionResult EditProduct(String id)
        {
            sanPham sP = db.sanPhams.SingleOrDefault(n => n.maSP == id);
            if (sP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.maLoai = new SelectList(db.loaiSPs.ToList().OrderBy(n => n.loaiHang), "maLoai", "loaiHang");
            return View(sP);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(sanPham sP, HttpPostedFileBase fileupload)
        {
            ViewBag.maLoai = new SelectList(db.loaiSPs.ToList().OrderBy(n => n.loaiHang), "maLoai", "loaiHang");
            sanPham p = db.sanPhams.Where(s => s.maSP == sP.maSP).First();
            p.tenSP = sP.tenSP;
            p.ngayDang = sP.ngayDang;
            p.noiDung = sP.noiDung;
            p.giaBan = sP.giaBan;
            p.giamGia = sP.giamGia;
            p.gioiTinh = sP.gioiTinh;
            p.maLoai = sP.maLoai;
            p.dvt = sP.dvt;
            if (fileupload !=null && fileupload.ContentLength >0)
            {
                string id = sP.maSP;
                string _FileName = "";
                int index = fileupload.FileName.IndexOf('.');
                _FileName = "sP" + id.ToString() + "." + fileupload.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("/Asset/products/"), _FileName);
                fileupload.SaveAs(_path);
                p.hinhDD = _FileName;
            }
            db.SaveChanges();
            return RedirectToAction("Products");
        }
        public ActionResult Users()
        {
            return View(db.taiKhoanTVs.ToList());
        }
        public ActionResult UserProfile(int id)
        {
            taiKhoanTV tk = db.taiKhoanTVs.SingleOrDefault(n => n.maKH == id);
            ViewBag.maKH = tk.maKH;
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tk);
        }
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            taiKhoanTV tk = db.taiKhoanTVs.SingleOrDefault(n => n.maKH == id);
            ViewBag.maKH = tk.maKH;
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tk);
        }
        [HttpPost, ActionName("DeleteUser")]
        public ActionResult ConfirmDeleteUser(int id)
        {
            taiKhoanTV tk = db.taiKhoanTVs.SingleOrDefault(n => n.maKH == id);
            ViewBag.maKH = tk.maKH;
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.taiKhoanTVs.Remove(tk);
            db.SaveChanges();
            return RedirectToAction("Users","Admin");
        }
        public ActionResult Orders()
        {
            return View(db.donHangs.ToList());
        }
        public ActionResult OrderDetails(int id)
        {
            List<ctDonHang> dh = db.ctDonHangs.Where(n => n.soDH == id).ToList(); ;
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }
        [HttpGet]
        public ActionResult DeleteOrder(int id)
        {
            List<ctDonHang> dh = db.ctDonHangs.Where(n => n.soDH == id).ToList(); ;
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }
        [HttpPost, ActionName("DeleteOrder")]
        public ActionResult ConfirmDelete(int id)
        {
            List<ctDonHang> ctdh = db.ctDonHangs.Where(n => n.soDH == id).ToList();
            donHang dh = db.donHangs.SingleOrDefault(n => n.soDH == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            foreach(var item in ctdh)
            {
                db.ctDonHangs.Remove(item);
            }
            db.donHangs.Remove(dh);
            db.SaveChanges();
            return RedirectToAction("Orders","Admin");

        }
        public ActionResult AdminProfile()
        {
            return View();
        }
    }

}