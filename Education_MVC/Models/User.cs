using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class User
    {
        [Key]
        public string id { get; set; }
        public double balance { get; set; }
    }
}