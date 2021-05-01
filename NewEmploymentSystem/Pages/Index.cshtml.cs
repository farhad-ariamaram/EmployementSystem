using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(string id)
        {
            //گرفتن لول کاربر جاری
            var checkPreInfoId = from a in _db.TblUsers
                                 where a.Id.Equals(id)
                                 select a.CurrentLevel;

            if (id == null)
            {
                checkPreInfoId = from a in _db.TblUsers
                                 where a.Id.Equals(HttpContext.Session.GetString("uid"))
                                 select a.CurrentLevel;
                //از کنترلر اومده
                if (HttpContext.Session.GetString("uid") != null)
                {
                    var currentLevel = checkPreInfoId.FirstOrDefault();

                    //اگه خالی بود یعنی مرحله یکه
                    if (currentLevel == null)
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.One;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "وان" بود یعنی مرحله یکشو کامل کرده و الان مرحله دومه
                    if (currentLevel.Equals("One"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Two;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "توو" بود یعنی مرحله دومشو کامل کرده و الان مرحله سومه
                    if (currentLevel.Equals("Two"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Three;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "تری" بود یعنی مرحله سومشو کامل کرده و الان مرحله چهارمه
                    if (currentLevel.Equals("Three"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Four;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "فور" بود یعنی مرحله چهارمش کامل کرده و الان مرحله پنجمه
                    if (currentLevel.Equals("Four"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Five;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "فایو" بود یعنی مرحله پنجمو کامل کرده و الان مرحله ششمه
                    if (currentLevel.Equals("Five"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Six;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "سیکس" بود یعنی مرحله ششمو کامل کرده و الان مرحله هفتمه
                    if (currentLevel.Equals("Six"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Seven;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "سون" بود یعنی مرحله هفتمو کامل کرده و الان مرحله هشتمه
                    if (currentLevel.Equals("Seven"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Eight;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "ایت" بود یعنی مرحله هشتمو کامل کرده و الان مرحله نهمه
                    if (currentLevel.Equals("Eight"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Nine;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "ناین" بود یعنی مرحله نهمو کامل کرده و الان مرحله دهمه
                    if (currentLevel.Equals("Nine"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Ten;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "تن" بود یعنی مرحله دهم کامل کرده و الان مرحله یازدهم
                    if (currentLevel.Equals("Ten"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Eleven;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "الون" بود یعنی مرحله یازدهم کامل کرده و الان مرحله دوازدهم
                    if (currentLevel.Equals("Eleven"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Twelve;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "توالو" بود یعنی مرحله دوازدهم کامل کرده و الان مرحله سیزدهم
                    if (currentLevel.Equals("Twelve"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Thirteen;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "ترتین" بود یعنی مرحله سیزدهم کامل کرده و الان مرحله چهاردهم
                    if (currentLevel.Equals("Thirteen"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Fourteen;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "فورتین" بود یعنی مرحله چهاردهم کامل کرده و الان مرحله پانزدهم
                    if (currentLevel.Equals("Fourteen"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Fifteen;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    //اگه "فیفتیین" بود یعنی مرحله پونزدهم کامل کرده و الان مرحله شانزدهم
                    if (currentLevel.Equals("Fifteen"))
                    {
                        var q = from a in _db.TblPagesSequences
                                where a.Status == true
                                select a.Sixteen;

                        var number = q.FirstOrDefault();

                        return RedirectToPage("Level" + number + "/Index");
                    }

                    return RedirectToPage("Level/Index");
                }

                //ای دی نداره
                return Page();
            }


            //چک کن اگر آی دی وارد شده وجود داشت ولی اشتباه بود برو صفحه 404
            var checkId = from a in _db.TblLinks
                          where a.Id.Equals(id)
                          select a;
            if (!checkId.Any())
            {
                return RedirectToPage("NotFound");
            }

            //ای دی درسته و دفعه اولشه
            if (checkPreInfoId.FirstOrDefault() == null)
            {
                HttpContext.Session.SetString("uid", id);

                var q = from a in _db.TblPagesSequences
                        where a.Status == true
                        select a.One;

                var number = q.FirstOrDefault();

                return RedirectToPage("Level" + number + "/Index");
            }

            //ای دی درسته و دفعه اولش نیست
            return Page();

        }

        [BindProperty] public Login login { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //اگر نام کاربری یا پسورد اشتباه بود خطا نشان بده
            var q = from a in _db.TblUsers
                    where ((a.Username == login.username.toEnglishNumber()) || ("0" + a.Username == login.username.toEnglishNumber())) && (a.Password == login.password.toEnglishNumber())
                    select a;
            if (!q.Any())
            {
                ModelState.AddModelError("WrongUP", "نام کاربری یا کلمه عبور اشتباه است");
                return Page();
            }

            //این سشن مشخص میکنه که کاربر لاگینه یا نه
            string uid = q.FirstOrDefault().Id;
            HttpContext.Session.SetString("uid", uid);

            //log ip
            TblIpLog tblip = new TblIpLog()
            {
                DateTime = DateTime.Now,
                UserId = uid,
                Ip = HttpContext.Connection.RemoteIpAddress.ToString()
            };
            _db.TblIpLogs.Add(tblip);
            _db.SaveChanges();

            //ای دی را خالی میکنیم تا وارد کنترلر شود
            return RedirectToPage("Index", new { id = "" });
        }


    }

    public class Login
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string username { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string password { get; set; }
    }
}
