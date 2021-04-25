using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages.Level7
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.TblUserMilitary military { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);

            //اگر زن بود برگرده
            var q = _db.TblPrimaryInformations.FirstOrDefault(s => s.UserId == uid);
            if (q.Gender.Equals("خانم"))
            {
                TblUserMilitary t = new TblUserMilitary()
                {
                    UserId = uid
                };
                _db.TblUserMilitaries.Add(t);
                _db.SaveChanges();

                TblUser user1 = _db.TblUsers.Find(uid);
                string currentUserLevel1 = user1.CurrentLevel;
                string nextUserLevel1 = null;
                nextUserLevel1 = Utility.findNextPagesSequence(currentUserLevel1);
                user1.CurrentLevel = nextUserLevel1;
                _db.SaveChanges();
                return RedirectToPage("../Index");
            }

            //اگر زیر 18 سال بود برگرده
            
            if ((DateTime.Now.Year - ((DateTime)q.BirthDate).Year) < 18)
            {
                TblUserMilitary t = new TblUserMilitary()
                {
                    UserId = uid,
                    MilitaryId = 9
                };
                _db.TblUserMilitaries.Add(t);
                _db.SaveChanges();

                TblUser user1 = _db.TblUsers.Find(uid);
                string currentUserLevel1 = user1.CurrentLevel;
                string nextUserLevel1 = null;
                nextUserLevel1 = Utility.findNextPagesSequence(currentUserLevel1);
                user1.CurrentLevel = nextUserLevel1;
                _db.SaveChanges();
                return RedirectToPage("../Index");
            }


            ViewData["MilitaryId"] = new SelectList(_db.TblMilitaries, "Id", "MilitaryStatus");

            if (HttpContext.Session.GetString("EnterLevel7") == null)
            {
                HttpContext.Session.SetString("EnterLevel7", DateTime.Now.ToString());
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string uid = HttpContext.Session.GetString("uid");
            if (!ModelState.IsValid)
            {
                ViewData["MilitaryId"] = new SelectList(_db.TblMilitaries, "Id", "MilitaryStatus");
                ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);
                return Page();
            }


            

            if (military.MilitaryId == 1) //غیبت
            {
                military.UserId = uid;
                _db.TblUserMilitaries.Add(military);
                _db.SaveChanges();

                TblUser user1 = _db.TblUsers.Find(uid);
                string currentUserLevel1 = user1.CurrentLevel;
                string nextUserLevel1 = null;
                nextUserLevel1 = Utility.findNextPagesSequence(currentUserLevel1);
                user1.CurrentLevel = nextUserLevel1;
                _db.SaveChanges();

                //Time Logging
                _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel7"), "Level7"));
                _db.SaveChanges();

                return RedirectToPage("../Index");
            }

            if (military.MilitaryId == 9) //مشمول نیستم
            {
                military.UserId = uid;
                _db.TblUserMilitaries.Add(military);
                _db.SaveChanges();

                TblUser user1 = _db.TblUsers.Find(uid);
                string currentUserLevel1 = user1.CurrentLevel;
                string nextUserLevel1 = null;
                nextUserLevel1 = Utility.findNextPagesSequence(currentUserLevel1);
                user1.CurrentLevel = nextUserLevel1;
                _db.SaveChanges();

                //Time Logging
                _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel7"), "Level7"));
                _db.SaveChanges();

                return RedirectToPage("../Index");
            }

            HttpContext.Session.SetString("MilitaryStatus", military.MilitaryId + "");
            return RedirectToPage("MilitaryDetail");
        }
    }
}