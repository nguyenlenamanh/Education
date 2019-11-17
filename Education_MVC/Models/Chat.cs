﻿using System;
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
        public int MaLH { get; set; }
        public long Hash { get; set; }

        public virtual NguoiHoc NguoiHoc { get; set; }

        public virtual LopHoc  LopHoc { get; set; }
    }
}