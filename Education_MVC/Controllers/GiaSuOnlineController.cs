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
                User u1 = db.Users.Single(x => x.id == idUser);
                u1.balance = u.balance - (hocPhi + hocPhi * 0.1);
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
                //return Json(new { hash = Guid.NewGuid(), JsonRequestBehavior.AllowGet });
            }
            
        }
        public bool checkExists(int idLH,string idNH)
        {
            db = new GiaSuOnlineDB();
            LopHoc lh = db.LopHocs.SingleOrDefault(x => x.MaLH == idLH);
            if (lh.Chats.SingleOrDefault(x => x.MaNH == idNH) != null) return true;
            return false;
        }

        public bool checkTeachExists(int idLH, string idNH)
        {
            db = new GiaSuOnlineDB();
            LopHoc lh = db.LopHocs.SingleOrDefault(p => p.MaLH == idLH && p.MaGS == idNH);
            if (lh.Chats.SingleOrDefault(x => x.MaNH == idNH) != null) return true;
            return false;
        }





        public ActionResult Chat(int id)
        {
            ApplicationDbContext dbIdentity = new ApplicationDbContext();
            if (string.IsNullOrWhiteSpace(User.Identity.Name)) return View("~/Views/Error.cshtml");

            string idUser = dbIdentity.Users.Single(x => x.UserName == User.Identity.Name).Id;

            db = new GiaSuOnlineDB();
            Chat chat = db.Chats.Single(p => p.Hash == id);
            if (chat == null) return View("~/Views/Error.cshtml");

            string studentID = chat.MaNH;

            string teacherID = chat.LopHoc.GiaSu.MaGS;

            if (studentID == idUser || teacherID == idUser)
            {
                if (studentID == idUser)
                {
                    ViewBag.Name = db.GiaSus.SingleOrDefault(p => p.MaGS == teacherID).TenGS;
                    DisplayChatUser d = getChatUser(teacherID, chat.ChatID, id, true);
                    if (d != null) ViewBag.CurrentChat = d;
                    else ViewBag.CurrentChat = new DisplayChatUser() { Hash = -1 };

                    List<DisplayChatUser> displayChats = getAllChatOfStudent(studentID);
                    //find and remove current chat of all chats because we already display current chat so dont need to display it twice
                    if (displayChats != null && d != null)
                    {
                        var found = displayChats.SingleOrDefault(p => p.Hash == d.Hash);
                        displayChats.Remove(found);
                    }

                    ViewBag.ChatWithID = teacherID;
                    if (displayChats != null)
                        ViewBag.ListChats = displayChats;
                    else ViewBag.ListChats = new List<DisplayChatUser>();
                }
                else
                {
                    ViewBag.Name = db.NguoiHocs.SingleOrDefault(p => p.MaNH == studentID).TenNH;
                    DisplayChatUser d = getChatUser(studentID, chat.ChatID, id, false);
                    ViewBag.CurrentChat = d;

                    List<DisplayChatUser> displayChats = getAllChatOfTeacher(teacherID);
                    //find and remove current chat of all chats because we already display current chat so dont need to display it twice
                    if (displayChats != null && d != null)
                    {
                        var found = displayChats.SingleOrDefault(p => p.Hash == d.Hash);
                        displayChats.Remove(found);
                    }

                    ViewBag.ChatWithID = studentID;
                    ViewBag.ListChats = displayChats;
                }

                ViewBag.UserID = idUser;
                ViewBag.ClassID = id;

                ViewBag.ChatContents = chatContents(chat.ChatID);

                return View();
                // return View();
            }

            return View("~/Views/Error.cshtml");
        }

        public List<DisplayChatUser> getAllChatOfTeacher(string teacherID)
        {
            db = new GiaSuOnlineDB();

            var gs = db.GiaSus.SingleOrDefault(p => p.MaGS == teacherID);

            List<Chat> chats = new List<Chat>();

            foreach (LopHoc lh in gs.LopHocs)
            {
                var chatCollections = db.Chats.Where(p => p.MaLH == lh.MaLH);

                foreach (Chat c in chatCollections)
                {
                    chats.Add(c);
                }
            }


            List<DisplayChatUser> displaychats = new List<DisplayChatUser>();

            if (chats.Count == 0) return null;

            foreach (Chat c in chats)
            {
                string studentName = db.NguoiHocs.Single(p => p.MaNH == c.MaNH).TenNH;

                var least = db.ChatDetails.Where(p => p.ChatID == c.ChatID).OrderByDescending(p => p.ChatDate).FirstOrDefault();

                if (least == null) return null;

                string content = StringCipher.Decrypt(least.Content,least.ChatUser);
                //string content = least.Content;
                DateTime date = least.ChatDate;

                displaychats.Add(new DisplayChatUser { Hash = c.Hash, LastestChatDate = date, LastestContent = content, Name = studentName });
            }

            return displaychats;
        }

        public List<DisplayChatUser> getAllChatOfStudent(string studentID)
        {
            db = new GiaSuOnlineDB();

            var student = db.NguoiHocs.SingleOrDefault(p => p.MaNH == studentID);

            var chats = db.Chats.Where(p => p.MaNH == studentID);

            List<DisplayChatUser> displaychats = new List<DisplayChatUser>();

            foreach (Chat c in chats)
            {
                string teacherName = db.GiaSus.Single(p => p.MaGS == c.LopHoc.MaGS).TenGS;

                var least = db.ChatDetails.Where(p => p.ChatID == c.ChatID).OrderByDescending(p => p.ChatDate).FirstOrDefault();

                if (least == null) return null;

                string content = StringCipher.Decrypt(least.Content, least.ChatUser);
                //string content = least.Content;
                DateTime date = least.ChatDate;

                displaychats.Add(new DisplayChatUser { Hash = c.Hash, LastestChatDate = date, LastestContent = content, Name = teacherName });
            }

            return displaychats;
        }

        public DisplayChatUser getChatUser(string userID, int chatID, long hash, bool getTeacher = false)
        {
            db = new GiaSuOnlineDB();

            var least = db.ChatDetails.Where(p => p.ChatID == chatID).OrderByDescending(p => p.ChatDate).FirstOrDefault();

            if (least == null) return null;

            string name = "";

            if (getTeacher) name = db.GiaSus.Single(p => p.MaGS == userID).TenGS;
            else name = db.NguoiHocs.Single(p => p.MaNH == userID).TenNH;

            //var content = least.Content;
            string content = StringCipher.Decrypt(least.Content, least.ChatUser);
            //string content = least.Content;
            var date = least.ChatDate;

            return new DisplayChatUser { Name = name, Hash = hash, LastestChatDate = date, LastestContent = content };
        }

        public List<ChatDetail> chatContents(int chatID)
        {
            db = new GiaSuOnlineDB();

            List<ChatDetail> test = db.ChatDetails.Where(p => p.ChatID == chatID).OrderBy(p => p.ChatDate).ToList().Select(x => new ChatDetail
            {
                ChatDate = x.ChatDate,
                ChatDetailID = x.ChatDetailID,
                ChatID = x.ChatID,
                ChatUser = x.ChatUser,
                Content = StringCipher.Decrypt(x.Content, x.ChatUser)
            }).ToList();

            return test;
        }
    }
}