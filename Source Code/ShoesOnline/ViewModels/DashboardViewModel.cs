using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesOnline.ViewModels
{
    public class DashboardViewModel
    {
        public int products_count { get; set; }
        public int orders_count { get; set; }
        public int users_count { get; set; }
    }
}