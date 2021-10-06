using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoesOnline.Models;

using PagedList;
using PagedList.Mvc;
namespace ShoesOnline.Controllers
{
    public class BoyProductsController : Controller
    {
        // GET: BoyProducts
        ShoesOnlineEntities data = new ShoesOnlineEntities();
        private List<sanPham> TakeBoyProducts(int n)
        {

            return data.sanPhams.Where(a => a.gioiTinh == 3).Take(n).ToList();
        }
        public ActionResult Index(int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var BoyProducts = TakeBoyProducts(12);
            return View(BoyProducts.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Collections()
        {
            var Collections = from loaiSP in data.loaiSPs
                              where loaiSP.maLoai == 6 ||
                                    loaiSP.maLoai == 4 ||
                                    loaiSP.maLoai == 3
                              select loaiSP;
            return PartialView(Collections);
        }
        public ActionResult BoyProductsByCollections(int id)
        {
            var Products = from h in data.sanPhams where h.maLoai == id && h.gioiTinh == 3 select h;
            return View(Products);
        }
        public ActionResult Details(String id)
        {
            ShoesOnlineEntities db = new ShoesOnlineEntities();
            var Products = from z in db.sanPhams
                           where z.maSP == id
                           select z;
            return View(Products.SingleOrDefault());
        }
    }
}