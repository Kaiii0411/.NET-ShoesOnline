using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesOnline.Models
{
    public class CommonInfo
    {
        private ShoesOnlineEntities db;
        public CommonInfo()
        {
            this.db = new ShoesOnlineEntities();
        }
        public IEnumerable<sanPham> MenProductsHunter(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 1 && x.maLoai == 1).Take(n).ToList();
        }
        public IEnumerable<sanPham> MenProductsSandal(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 1 && x.maLoai == 3).Take(n).ToList();
        }
        public IEnumerable<sanPham> MenProductsSport(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 1 && x.maLoai == 6).Take(n).ToList();
        }
        public IEnumerable<sanPham> WomenProductsSandal(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 2 && x.maLoai == 3).Take(n).ToList();
        }
        public IEnumerable<sanPham> WomenProductsRunning(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 2 && x.maLoai == 2).Take(n).ToList();
        }
        public IEnumerable<sanPham> WomenProductsFashion(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 2 && x.maLoai == 8).Take(n).ToList();
        }
        public IEnumerable<sanPham> WomenProductsHunter(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 2 && x.maLoai == 1).Take(n).ToList();
        }
        public IEnumerable<sanPham> BoyProductsSandal(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 3 && x.maLoai == 3).Take(n).ToList();
        }
        public IEnumerable<sanPham> BoyProductsSlipper(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 3 && x.maLoai == 4).Take(n).ToList();
        }
        public IEnumerable<sanPham> BoyProductsSport(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 3 && x.maLoai == 6).Take(n).ToList();
        }
        public IEnumerable<sanPham> GirlProductsSandal(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 4 && x.maLoai == 3).Take(n).ToList();
        }
        public IEnumerable<sanPham> GirlProductsSlipper(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 4 && x.maLoai == 4).Take(n).ToList();
        }
        public IEnumerable<sanPham> GirlProductsSport(int n)
        {
            return db.sanPhams.Where(x => x.gioiTinh == 4 && x.maLoai == 6).Take(n).ToList();
        }
        public IEnumerable<loaiSP> MenCollections
        {
            get
            {
                return db.loaiSPs.Where(x => x.maLoai == 1 ||
                                        x.maLoai == 2 ||
                                        x.maLoai == 3 ||
                                        x.maLoai == 5 ||
                                        x.maLoai == 4 ||
                                        x.maLoai == 6 ||
                                        x.maLoai == 7).ToList();
            }
        }
        public IEnumerable<loaiSP> WomenCollections
        {
            get
            {
                return db.loaiSPs.Where(x => x.maLoai == 1 ||
                                        x.maLoai == 2 ||
                                        x.maLoai == 3 ||
                                        x.maLoai == 4 ||
                                        x.maLoai == 6 ||
                                        x.maLoai == 8).ToList();
            }
        }
        public IEnumerable<loaiSP> BoyCollections
        {
            get
            {
                return db.loaiSPs.Where(x => x.maLoai == 4 ||
                                        x.maLoai == 6 ||
                                        x.maLoai == 3).ToList();
            }
        }
        public IEnumerable<loaiSP> GirlCollections
        {
            get
            {
                return db.loaiSPs.Where(x => x.maLoai == 4 ||
                                        x.maLoai == 6 ||
                                        x.maLoai == 3).ToList();
            }
        }
    }
}