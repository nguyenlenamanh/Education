using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class GiaSuOnlineDB : DbContext
    {
        public GiaSuOnlineDB() : base("GiaSuOnlineDB") { }
        public DbSet<GiaSu> GiaSus { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<NguoiHoc> NguoiHocs { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatDetail> ChatDetails { get; set; }

        public DbSet<DanhGiaCuaNguoiHoc> DanhGiaNguoiHocs { get; set; }
    }
}