using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages.Level13
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        public IEnumerable<TblUserCreativity> tblUserCreativity { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);

            tblUserCreativity = from a in _db.TblUserCreativities
                                where a.UserId == uid
                                select a;

            if (HttpContext.Session.GetString("EnterLevel13") == null)
            {
                HttpContext.Session.SetString("EnterLevel13", DateTime.Now.ToString());
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string uid = HttpContext.Session.GetString("uid");

            //برای اینکه اگر خواست بدون اضافه کردن از این مرحله رد بشه
            //این باعث میشه دیگه یدونه خالی ایجاد نکنه اگه داشت
            tblUserCreativity = from a in _db.TblUserCreativities
                                where a.UserId == uid
                                select a;
            if (tblUserCreativity.Any())
            {
                //Time Logging
                _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel13"), "Level13"));
                _db.SaveChanges();

                TblUser user1 = _db.TblUsers.Find(uid);
                string currentUserLevel1 = user1.CurrentLevel;
                string nextUserLevel1 = null;
                nextUserLevel1 = Utility.findNextPagesSequence(currentUserLevel1);
                user1.CurrentLevel = nextUserLevel1;
                _db.SaveChanges();
                return RedirectToPage("../Index");
            }

            TblUserCreativity t = new TblUserCreativity()
            {
                UserId = uid
            };
            _db.TblUserCreativities.Add(t);
            _db.SaveChanges();

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel13"), "Level13"));
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