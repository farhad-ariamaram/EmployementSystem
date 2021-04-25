using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages.Level6
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        public IEnumerable<TblUserJob> userJob { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);

            userJob = from a in _db.TblUserJobs
                      where a.UserId == uid
                      select a;

            if (HttpContext.Session.GetString("EnterLevel6") == null)
            {
                HttpContext.Session.SetString("EnterLevel6", DateTime.Now.ToString());
            }
  
            return Page();
        }

        public IActionResult OnPost()
        {
            string uid = HttpContext.Session.GetString("uid");

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel6"), "Level6"));
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