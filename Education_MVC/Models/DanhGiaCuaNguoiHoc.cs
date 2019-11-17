using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class DanhGiaCuaNguoiHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string MaNH { get; set; }
        public string DanhGia { get; set; }
        public virtual NguoiHoc  NguoiHoc { get; set; }
    }
}