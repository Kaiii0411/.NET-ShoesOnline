//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class quanHuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quanHuyen()
        {
            this.KhachHangs = new HashSet<KhachHang>();
            this.taiKhoanTVs = new HashSet<taiKhoanTV>();
        }
    
        public int maQH { get; set; }
        public string tenQH { get; set; }
        public string tinhThanh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taiKhoanTV> taiKhoanTVs { get; set; }
    }
}
