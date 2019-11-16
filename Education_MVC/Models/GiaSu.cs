using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class GiaSu
    {
        [Key]
        public string MaGS { get; set; }
        public string TenGS { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public virtual List<LopHoc> LopHocs { get; set; }
    }
}