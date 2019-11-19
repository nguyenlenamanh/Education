using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class ChatDetail
    {
        public int ChatDetailID { get; set; }
        public string Content { get; set; }
        public string ChatUser { get; set; }
        public DateTime ChatDate { get; set; }

        public int ChatID { get; set; }
        public virtual Chat Chat { get; set; }
    }
}