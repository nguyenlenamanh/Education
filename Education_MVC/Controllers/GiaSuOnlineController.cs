using Education_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Education_MVC.Controllers
{
    public class GiaSuOnlineController : Controller
    {
        GiaSuOnlineDB db;
        // GET: GiaSuOnline
        public ActionResult Index()
        {
            db = new GiaSuOnlineDB();
            return View(db.LopHocs);
        }
        //[ChildActionOnly]
        public ActionResult DanhGiaCuaNguoiHoc()
        {
            db = new GiaSuOnlineDB();
            return PartialView("DanhGiaCuaNguoiHoc",db.DanhGiaNguoiHocs);
        }
        [Authorize]
        public ActionResult ThanhToan(int id)
        {
            db = new GiaSuOnlineDB();
            return View(db.LopHocs.SingleOrDefault(x=>x.MaLH == id));
        }
        [Authorize]
        public ActionResult XacNhanThanhToan(int id)
        {
            db = new GiaSuOnlineDB();
            ApplicationDbContext dbIdentity = new ApplicationDbContext();
            long hash = new Random().Next();
            //MD5 md5 = MD5.Create();
            //md5.Initialize();
            //md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
            LopHoc lh = db.LopHocs.SingleOrDefault(x => x.MaLH == id);
            double hocPhi = lh.Gia;
            string idUser = dbIdentity.Users.Single(x => x.UserName == User.Identity.Name).Id;
            User u = db.Users.Single(x => x.id == idUser);
            if (checkExists(id,u.id) == true) return Json(new { result = "Bạn đã đăng ký khóa học này rồi" }, JsonRequestBehavior.AllowGet);  
            if (hocPhi + hocPhi * 0.1 > u.balance) return Json(new { result = "Không đủ số dư để thanh toán" }, JsonRequestBehavior.AllowGet);
            else
            {
                User gsUser = db.Users.Single(x => x.id == lh.GiaSu.MaGS);
                u.balance = u.balance - (hocPhi + hocPhi * 0.1);
                gsUser.balance = gsUser.balance + (hocPhi - hocPhi * 0.1);

                Chat c = new Chat()
                {
                    MaLH = id,
                    MaNH = idUser,
                    Hash = hash
                };
                ThanhToan t = new ThanhToan()
                {
                    MaLH = id,
                    MaNH = u.id,
                    SoTienTT = hocPhi + hocPhi * 0.1,
                };
                db.ThanhToans.Add(t);
                db.Chats.Add(c);
                db.SaveChanges();
                return Json(new { hash = c.Hash},JsonRequestBehavior.AllowGet);
            }
            
        }
        public bool checkExists(int idLH,string idNH)
        {
            db = new GiaSuOnlineDB();
            LopHoc lh = db.LopHocs.SingleOrDefault(x => x.MaLH == idLH);
            if (lh.Chats.SingleOrDefault(x => x.MaNH == idNH) != null) return true;
            return false;
        }
    }
}