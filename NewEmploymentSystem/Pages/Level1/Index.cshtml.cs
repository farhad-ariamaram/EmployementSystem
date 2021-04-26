using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages.Level1
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            //چک کن اگه لاگین نبود بفرستش صفحه لاگین
            string uid = HttpContext.Session.GetString("uid");
            if (uid == null)
            {
                return RedirectToPage("Index");
            }

            //log time
            if (HttpContext.Session.GetString("EnterLevel1") == null)
            {
                HttpContext.Session.SetString("EnterLevel1", DateTime.Now.ToString());
            }

            return Page();
        }

        [BindProperty]
        public TblPrimaryInformation primaryInformation { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!_NationalCodeCheck(primaryInformation.NationalCode))
            {
                ModelState.AddModelError("NationalCode", "کد ملی وارد شده صحیح نمی‌باشد");
                return Page();
            }

            string uid = HttpContext.Session.GetString("uid");

            //بر اساس اطلاعات اولیه یک کاربر جدید بساز
            //کارنت" و سیستمی که الان فعاله رو براش ست کن"
            var currentPagesSequence = from a in _db.TblPagesSequences  //بدست اوردن سیستم ترتیب صفحه جاری
                                       where a.Status == true
                                       select a.Id;
            var currentUserPhoneNo = from a in _db.TblLinks  //بدست اوردن شماره کاربر جاری
                                     where a.Id == uid
                                     select a.Phone;
            TblUser tblUser = new TblUser()
            {
                Id = uid,
                CurrentLevel = "One",
                PagesSequenceId = currentPagesSequence.FirstOrDefault(),
                Username = currentUserPhoneNo.FirstOrDefault().Substring(3),
                Password = primaryInformation.NationalCode
            };
            _db.TblUsers.Add(tblUser);
            _db.SaveChanges();

            //log ip
            TblIpLog tblip = new TblIpLog()
            {
                DateTime = DateTime.Now,
                UserId = uid,
                Ip = HttpContext.Connection.RemoteIpAddress.ToString()
            };
            _db.TblIpLogs.Add(tblip);
            _db.SaveChanges();

            //اطلاعات اولیه را وارد کن
            PersianCalendar pc = new PersianCalendar();
            string[] birthDate = Request.Form["BirthDate"].ToString().Split("/");
            DateTime bd = new DateTime(int.Parse(birthDate[0]), int.Parse(birthDate[1]), int.Parse(birthDate[2]), pc);
            var phoneNo = from a in _db.TblLinks
                          where a.Id == uid
                          select a.Phone;
            TblPrimaryInformation tblPrimaryInformation = new TblPrimaryInformation()
            {
                FirstName = primaryInformation.FirstName,
                LastName = primaryInformation.LastName,
                NationalCode = primaryInformation.NationalCode.toEnglishNumber(),
                BirthDate = bd,
                PhoneNo = phoneNo.FirstOrDefault(),
                Gender = Request.Form["gender"].ToString(),
                Marital = Request.Form["marital"].ToString(),
                ChildrenNo = primaryInformation.ChildrenNo,
                Tutelage = primaryInformation.Tutelage,
                UserId = uid
            };
            _db.TblPrimaryInformations.Add(tblPrimaryInformation);
            _db.SaveChanges();

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel1"), "Level1"));
            _db.SaveChanges();

            //دوباره برگرد به ایندکس تا کنترل بشه بره مرحله بعد
            return RedirectToPage("../Index");
        }

        //تابع چک کردن کد ملی
        private bool _NationalCodeCheck(string strNatinal)
        {
            if (strNatinal == "0000000000" || strNatinal == "1111111111" || strNatinal == "2222222222" || strNatinal == "3333333333" || strNatinal == "4444444444" || strNatinal == "5555555555" || strNatinal == "6666666666" || strNatinal == "7777777777" || strNatinal == "8888888888" || strNatinal == "9999999999")
                return false;

            int check = int.Parse(strNatinal[9].ToString());
            long sum = 0;
            for (int i = 0; i < 9; ++i)
            {
                sum += int.Parse(strNatinal[i].ToString()) * (10 - i);
            }
            sum %= 11;
            return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
        }
    }
}