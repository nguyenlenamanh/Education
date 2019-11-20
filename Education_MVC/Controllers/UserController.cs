using Education_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education_MVC.Controllers
{
    public class UserController : Controller
    {
        GiaSuOnlineDB db;
        ApplicationDbContext dbIdentity;
        // GET: User
        public ActionResult Index()
        {
            db = new GiaSuOnlineDB();
            dbIdentity = new ApplicationDbContext();
            string id = dbIdentity.Users.SingleOrDefault(x => x.Email == User.Identity.Name).Id;
            if(id != null)
            {
                double balance = db.Users.SingleOrDefault(x => x.id == id).balance;
                if (CheckUser(id))
                {
                    GiaSu gs = db.GiaSus.SingleOrDefault(x => x.MaGS == id);
                    IEnumerable<ThanhToan> listThanhToan = db.ThanhToans.Where(x => x.MaNH == id);
                    List<LopHoc> LopHocDaMua = new List<LopHoc>();
                    foreach(var i in listThanhToan)
                    {
                        LopHocDaMua.Add(i.LopHoc);
                    }
                    UserDetail user = new UserDetail()
                    {
                        ID = gs.MaGS,
                        Email = gs.Email,
                        Phone = gs.SDT,
                        Balance = balance,
                        Name = gs.TenGS,
                        LopHocDaMua = LopHocDaMua,
                        LopHocDangDay = gs.LopHocs
                    };
                    return View(user);
                }
                else
                {
                    NguoiHoc nh = db.NguoiHocs.SingleOrDefault(x => x.MaNH == id);
                    IEnumerable<ThanhToan> listThanhToan = db.ThanhToans.Where(x => x.MaNH == id);
                    List<LopHoc> LopHocDaMua = new List<LopHoc>();
                    foreach (var i in listThanhToan)
                    {
                        LopHocDaMua.Add(i.LopHoc);
                    }
                    UserDetail user = new UserDetail()
                    {
                        ID = nh.MaNH,
                        Email = nh.Email,
                        Phone = nh.SDT,
                        Balance = balance,
                        Name = nh.TenNH,
                        LopHocDaMua = LopHocDaMua,
                        LopHocDangDay = new List<LopHoc>()                     
                    };
                    return View(user);
                }
            }
            return Json(new { status = "404" }, JsonRequestBehavior.AllowGet);
        }
        public bool CheckUser(string id)
        {
            db = new GiaSuOnlineDB();
            if (db.GiaSus.SingleOrDefault(x => x.MaGS == id) != null) return true;
            return false;
        }
    }
}