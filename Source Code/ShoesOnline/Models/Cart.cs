using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesOnline.Models
{
    public class Cart
    {
        ShoesOnlineEntities db = new ShoesOnlineEntities();
        public string SmaSP { get; set; }
        public string StenSP { get; set; }
        public string ShinhDD { get; set; }
        public int IgiaBan { get; set; }
        public int IsoLuong { get; set; }
        public int IthanhTien
        {
            get { return IsoLuong * IgiaBan; }
        }
        public Cart(String MaSP)
        {
            SmaSP = MaSP;
            sanPham Products = db.sanPhams.Single(m => m.maSP == SmaSP);
            StenSP = Products.tenSP;
            ShinhDD = Products.hinhDD;
            IgiaBan = int.Parse(Products.giaBan.ToString());
            IsoLuong = 1;
        }
    }
}