using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class NguoiHoc
    {
        [Key]
        public string MaNH { get; set; }
        public string TenNH { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public List<ThanhToan> ThanhToans { get; set; }
        public virtual List<Chat> Chats { get; set; }

        public virtual List<DanhGiaCuaNguoiHoc> DanhGias { get; set; }
    }
}