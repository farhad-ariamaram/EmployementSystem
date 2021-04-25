using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages.Level5
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        public IEnumerable<TblUserSkill> tblUserSkill { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);

            tblUserSkill = from a in _db.TblUserSkills
                           where a.UserId == uid
                           select a;

            if (HttpContext.Session.GetString("EnterLevel5") == null)
            {
                HttpContext.Session.SetString("EnterLevel5", DateTime.Now.ToString());
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string uid = HttpContext.Session.GetString("uid");
            tblUserSkill = from a in _db.TblUserSkills
                           where a.UserId == uid
                           select a;
            if (tblUserSkill.Any())
            {
                //Time Logging
                _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel5"), "Level5"));
                _db.SaveChanges();

                TblUser user1 = _db.TblUsers.Find(uid);
                string currentUserLevel1 = user1.CurrentLevel;
                string nextUserLevel1 = null;
                nextUserLevel1 = Utility.findNextPagesSequence(currentUserLevel1);
                user1.CurrentLevel = nextUserLevel1;
                _db.SaveChanges();
                return RedirectToPage("../Index");
            }

            TblUserSkill t = new TblUserSkill()
            {
                UserId = uid
            };
            _db.TblUserSkills.Add(t);
            _db.SaveChanges();

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel5"), "Level5"));
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