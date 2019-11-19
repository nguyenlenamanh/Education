using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class DisplayChatUser
    {
        public long Hash { get; set; }
        public string Name { get; set; }
        public string LastestContent { get; set; }
        public DateTime LastestChatDate { get; set; }
    }
}