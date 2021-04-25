using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages.Level2
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty] public TblUser user { get; set; }

        public IActionResult OnGet()
        {
            //چک کن اگه لاگین نبود بفرستش صفحه لاگین
            string uid = HttpContext.Session.GetString("uid");
            if (uid == null)
            {
                return RedirectToPage("Index");
            }

            //کاربر رو برای ویرایش پسورد با ای دی انتخاب کن
            user = _db.TblUsers.Find(uid);

            //log time
            if (HttpContext.Session.GetString("EnterLevel2") == null)
            {
                HttpContext.Session.SetString("EnterLevel2", DateTime.Now.ToString());
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            user.CurrentLevel = "Two";

            _db.Attach(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _db.SaveChanges();

            string uid = HttpContext.Session.GetString("uid");

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel2"), "Level2"));
            _db.SaveChanges();

            return RedirectToPage("../Index");
        }
    }
}