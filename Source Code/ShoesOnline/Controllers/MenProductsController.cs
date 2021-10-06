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
    public class MenProductsController : Controller
    {
        // GET: MenProducts
        ShoesOnlineEntities data = new ShoesOnlineEntities();
        private List<sanPham> TakeMenProducts(int n)
        {

            return data.sanPhams.Where(a => a.gioiTinh==1).Take(n).ToList();
        }
        public ActionResult Index(int ? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var MenProducts = TakeMenProducts(12);
            return View(MenProducts.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Collections()
        {
            var Collections = from loaiSP in data.loaiSPs
                              where loaiSP.maLoai == 1 ||
                                    loaiSP.maLoai == 2 ||
                                    loaiSP.maLoai == 3 ||
                                    loaiSP.maLoai == 5 ||
                                    loaiSP.maLoai == 4 ||
                                    loaiSP.maLoai == 6 ||
                                    loaiSP.maLoai == 7
                              select loaiSP;
            return PartialView(Collections);
        }
        public ActionResult MenProductsByCollections(int id)
        {
            var Products = from h in data.sanPhams where h.maLoai==id && h.gioiTinh==1 select h;
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