using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class ThanhToan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTT { get; set; }
        public double SoTienTT { get; set; }
        public string MaNH { get; set; }
        public int MaLH { get; set; }
        public virtual LopHoc LopHoc { get; set; }
        public virtual NguoiHoc NguoiHoc { get; set; }
    }
}