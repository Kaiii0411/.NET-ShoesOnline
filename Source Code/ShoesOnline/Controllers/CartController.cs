using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoesOnline.Models;

namespace ShoesOnline.Controllers
{
    public class CartController : Controller
    {
        ShoesOnlineEntities db = new ShoesOnlineEntities();
        // Lấy giỏ hàng
        public List<Cart> TakeCart()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                // Nếu chưa có thì khởi tạo listCart
                listCart = new List<Cart>();
                Session["Cart"] = listCart;
            }
            return listCart;
        }
        // Thêm hàng vào giỏ
        public ActionResult AddToCart(String SmaSP, String strURL)
        {
            List<Cart> listCart = TakeCart();
            Cart Product = listCart.Find(m => m.SmaSP == SmaSP);
            if (Product == null)
            {
                Product = new Cart(SmaSP);
                listCart.Add(Product);
                return Redirect(strURL);
            }
            else
            {
                Product.IsoLuong++;
                return Redirect(strURL);
            }
        }
        // Tổng số lượng sản phẩm
        private int Total()
        {
            int ITotal = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                ITotal = listCart.Sum(t => t.IsoLuong);
            }
            return ITotal;
        }
        // Tính tổng tiền 
        private int TotalMoney()
        {
            int ITotalMoney = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                ITotalMoney = listCart.Sum(t => t.IthanhTien);
            }
            return ITotalMoney;
        }
        // Xây dựng trang giỏ hàng
        public ActionResult Cart()
        {
            List<Cart> listCart = TakeCart();
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Total = Total();
            ViewBag.TotalMoney = TotalMoney();
            return View(listCart);
        }
        // Tạo Partial View để hiện thị thông tin giỏ hàng
        public ActionResult CartPartial()
        {
            ViewBag.Total = Total();
            ViewBag.TotalMoney = TotalMoney();
            return PartialView();
        }
        // Xóa giỏ hàng
        public ActionResult DeleteCart(String SMaSP)
        {
            List<Cart> listCart = TakeCart();
            Cart Product = listCart.SingleOrDefault(m => m.SmaSP == SMaSP);
            if (Product != null)
            {
                listCart.RemoveAll(m => m.SmaSP == SMaSP);
                return RedirectToAction("Cart");
            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        // Cập nhật giỏ hàng
        public ActionResult UpdateCart(String SMaSP, FormCollection f)
        {
            List<Cart> listCart = TakeCart();
            Cart Product = listCart.SingleOrDefault(m => m.SmaSP == SMaSP);
            if (Product != null)
            {
                Product.IsoLuong = int.Parse(f["txtAmount"].ToString());
            }
            return RedirectToAction("Cart");
        }
        public ActionResult ClearCart()
        {
            List<Cart> listCart = TakeCart();
            listCart.Clear();
            return RedirectToAction("Index", "Home");
        }
        // Hiển thị View đặt hàng để cập nhật các thông tin cho đơn hàng
        [HttpGet]
        public ActionResult Order()
        {
            // Kiểm tra đăng nhập
            if (Session["User"] == null || Session["User"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Lấy giỏ hàng từ Session
            List<Cart> listCart = TakeCart();
            ViewBag.Total = Total();
            ViewBag.TotalMoney = TotalMoney();
            return View(listCart);
        }
        public ActionResult Order(FormCollection collection)
        {
            // Thêm đơn hàng
            donHang dh = new donHang();
            taiKhoanTV tk = (taiKhoanTV)Session["User"];
            List<Cart> Cart = TakeCart();
            dh.maKH = tk.maKH;
            dh.taiKhoan = tk.taiKhoanKH;
            dh.tenKH = tk.tenTV;
            dh.ngayDat = DateTime.Now;
            dh.ngayGH = DateTime.Now.AddDays(15);
            db.donHangs.Add(dh);
            db.SaveChanges();
            // Thêm chi tiết đơn hàng
            foreach (var i in Cart)
            {
                ctDonHang ctdh = new ctDonHang();
                ctdh.soDH = dh.soDH;
                ctdh.maSP = i.SmaSP;
                ctdh.soLuong = i.IsoLuong;
                ctdh.giaBan = i.IgiaBan;
                db.ctDonHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("OrderConfirmation", "Cart");
        }
        public ActionResult OrderConfirmation()
        {
            return View();
        }

    }
}