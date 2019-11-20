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
            ApplicationDbContext db = new ApplicationDbContext();
            string id = db.Users.SingleOrDefault(x => x.UserName == "admin@example.com").Id;
            GiaSu g = new GiaSu()
            {
                TenGS = "ABC",
                DiaChi = "Quan 12",
                Email = "abc@gmail.com",
                MaGS = id,
                SDT = "123456"
            };
            NguoiHoc nh = new NguoiHoc()
            {
                TenNH = "Nguyen Van A",
                Email = "nguyenvana@gmail.com",
                MaNH = "123456",
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
                MoTa = "Lớp học về lập trình hướng sự kiện với công nghệ Java sẽ giúp cho các bạn nắm rõ hơn về cách thức lập trình cũng như tương tác sự kiện trong phần mềm, đi kèm với quá trình học tập sẽ là một dự án nhỏ, giúp chúng ta hiểu hơn về các vận hành thực tế của một dự án cũng như cách áp dụng những cái đã học vào thực tế sẽ như thế nào và gặp khó khăn ra sao. Sau khoá học này, tôi sẽ giúp các bạn giải quyết được việc đó",
                Img = "java",
            };
            LopHoc lh1 = new LopHoc()
            {
                MaGS = g.MaGS,
                TenLH = "Toán cao cấp 1",
                Gia = 200000,
                MoTa = "Lớp học về toán cao cấp 1, Là một môn học tiên khởi cho các học vấn về logic tính toán, giúp các bạn hiểu hơn về những thuật toán cũng như các phương pháp tính những phương trình bậc cao với cách thức giải quyết vô cùng phong phú, đảm bảo bạn sẽ rất thích thú với những gì mà ta sẽ học cũng như các áp dụng những kiến thức trên vào thực tế.",
                Img = "math",
            };
            LopHoc lh2 = new LopHoc()
            {
                MaGS = g.MaGS,
                TenLH = "Lập trình hướng sự kiện",
                Gia = 140000,
                MoTa = "Lớp học về lập trình hướng sự kiện với công nghệ C# sẽ giúp cho các bạn nắm rõ hơn về cách thức lập trình cũng như tương tác sự kiện trong phần mềm, đi kèm với quá trình học tập sẽ là một dự án nhỏ, giúp chúng ta hiểu hơn về các vận hành thực tế của một dự án cũng như cách áp dụng những cái đã học vào thực tế sẽ như thế nào và gặp khó khăn ra sao. Sau khoá học này, tôi sẽ giúp các bạn giải quyết được việc đó",
                Img = "c",
            };
            User u = new User()
            {
                id = g.MaGS,
                balance = 0
            };
            User u1 = new User()
            {
                id = "123456",
                balance = 0 
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