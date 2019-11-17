using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Education_MVC.Models
{
    public class DataInitialGS : CreateDatabaseIfNotExists<GiaSuOnlineDB>
    {
        protected override void Seed(GiaSuOnlineDB context)
        {
            GiaSu g = new GiaSu()
            {
                TenGS = "ABC",
                DiaChi = "Quan 12",
                Email = "abc@gmail.com",
                MaGS = "fe5d0669-552e-40ee-bf3f-cedabd3a5445",
                SDT = "123456"
            };
            NguoiHoc nh = new NguoiHoc()
            {
                TenNH = "Nguyen Van A",
                Email = "nguyenvana@gmail.com",
                MaNH   = "123456",
                SDT = "0123456798",
            };
            context.GiaSus.Add(g);
            context.NguoiHocs.Add(nh);
            context.SaveChanges();
            LopHoc lh = new LopHoc()
            {
                MaGS = g.MaGS,
                TenLH = "Lập trình hướng sự kiện",
                Gia = 100000,
                MoTa = "Lớp học về lập trình hướng sự kiện với công nghệ Java",
                Img = "java",
            };
            LopHoc lh1 = new LopHoc()
            {
                MaGS = g.MaGS,
                TenLH = "Toán cao cấp 1",
                Gia = 200000,
                MoTa = "Lớp học về toán cao cấp 1",
                Img = "math",
            };
            LopHoc lh2 = new LopHoc()
            {
                MaGS = g.MaGS,
                TenLH = "Lập trình hướng sự kiện",
                Gia = 140000,
                MoTa = "Lớp học về lập trình hướng sự kiện với công nghệ C#",
                Img = "c",
            };
            User u = new User()
            {
                id = "fe5d0669-552e-40ee-bf3f-cedabd3a5445",
                balance = 0
            };
            User u1 = new User()
            {
                id = "123456",
                balance = 500000
            };
            DanhGiaCuaNguoiHoc dg = new DanhGiaCuaNguoiHoc()
            {
                MaNH = "123456",
                DanhGia = "Khóa học rất bổ ích và xứng đáng với số tiền bỏ ra"
            };
            context.Users.Add(u);
            context.Users.Add(u1);
            context.DanhGiaNguoiHocs.Add(dg);
            context.LopHocs.Add(lh);
            context.LopHocs.Add(lh1);
            context.LopHocs.Add(lh2);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}