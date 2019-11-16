using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class Chat
    {
        [Key]
        [Column(Order = 1)]
        public string MaNH { get; set; }
        [Key]
        [Column(Order = 2)]
        public string MaGS { get; set; }
        public string Hash { get; set; }

        public virtual NguoiHoc NguoiHoc { get; set; }

        public virtual GiaSu GiaSu { get; set; }
    }
}