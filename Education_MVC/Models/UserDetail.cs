using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class UserDetail
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }

        public List<LopHoc> LopHocDangDay { get; set; }

        public List<LopHoc> LopHocDaMua { get; set; }
    }
}