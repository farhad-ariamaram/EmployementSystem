using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;
using Microsoft.EntityFrameworkCore;

namespace NewEmploymentSystem.Pages.Level15
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        public IEnumerable<TblUserLanguage> tblUserLanguage { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);

            tblUserLanguage = _db.TblUserLanguages.Include(a=>a.LanguageType).Where(a => a.UserId.Equals(uid));

            if (HttpContext.Session.GetString("EnterLevel15") == null)
            {
                HttpContext.Session.SetString("EnterLevel15", DateTime.Now.ToString());
            }

            if (!tblUserLanguage.Any())
            {
                return RedirectToPage("AddLanguage");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string uid = HttpContext.Session.GetString("uid");

            //برای اینکه اگر خواست بدون اضافه کردن از این مرحله رد بشه
            //این باعث میشه دیگه یدونه خالی ایجاد نکنه اگه داشت
            tblUserLanguage = _db.TblUserLanguages.Include(a => a.LanguageType).Where(a => a.UserId.Equals(uid));

            if (tblUserLanguage.Any())
            {
                //Time Logging
                _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel15"), "Level15"));
                _db.SaveChanges();

                TblUser user1 = _db.TblUsers.Find(uid);
                string currentUserLevel1 = user1.CurrentLevel;
                string nextUserLevel1 = null;
                nextUserLevel1 = Utility.findNextPagesSequence(currentUserLevel1);
                user1.CurrentLevel = nextUserLevel1;
                _db.SaveChanges();
                return RedirectToPage("../Index");
            }

            TblUserLanguage t = new TblUserLanguage()
            {
                UserId = uid
            };
            _db.TblUserLanguages.Add(t);
            _db.SaveChanges();

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel15"), "Level15"));
            _db.SaveChanges();

            TblUser user = _db.TblUsers.Find(uid);
            string currentUserLevel = user.CurrentLevel;
            string nextUserLevel = null;
            nextUserLevel = Utility.findNextPagesSequence(currentUserLevel);
            user.CurrentLevel = nextUserLevel;
            _db.SaveChanges();
            return RedirectToPage("../Index");
        }
    }
}