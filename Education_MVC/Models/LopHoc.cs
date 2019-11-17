using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class LopHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLH { get; set; }
        public string TenLH { get; set; }
        public double Gia { get; set; }
        public string MoTa { get; set; }
        public string MaGS { get; set; }
        public string Img { get; set; }
        public virtual GiaSu GiaSu { get; set; }
        public virtual List<ThanhToan> ThanhToans { get; set; }
        public virtual List<Chat> Chats { get; set; }
    }
}