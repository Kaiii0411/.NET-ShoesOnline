﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoesOnline.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShoesOnlineEntities : DbContext
    {
        public ShoesOnlineEntities()
            : base("name=ShoesOnlineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ctDonHang> ctDonHangs { get; set; }
        public virtual DbSet<donHang> donHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<loaiSP> loaiSPs { get; set; }
        public virtual DbSet<quanHuyen> quanHuyens { get; set; }
        public virtual DbSet<QuanTriVien> QuanTriViens { get; set; }
        public virtual DbSet<sanPham> sanPhams { get; set; }
        public virtual DbSet<taiKhoanTV> taiKhoanTVs { get; set; }
    }
}