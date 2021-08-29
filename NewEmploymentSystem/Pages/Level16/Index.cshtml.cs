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

namespace NewEmploymentSystem.Pages.Level16
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblUserSuggestion tblUserSuggestion { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            //check if uid not set  redirect to "Login"
            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);

            //find user with SESSION['uid']
            var queryyy = _db.TblUserSuggestions.FirstOrDefault(a=>a.UserId==uid);

            //check if already has track id redirect to "End"
            if (queryyy != null)
            {
                return RedirectToPage("../Index");
            }

            //log time
            if (HttpContext.Session.GetString("EnterLevel16") == null)
            {
                HttpContext.Session.SetString("EnterLevel16", DateTime.Now.ToString());
            }

            return Page();
        }

        public IActionResult OnPost()
        { 
            string uid = HttpContext.Session.GetString("uid");

            if (!ModelState.IsValid)
            {
                ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);
                return Page();
            }

            //set user postal code 
            tblUserSuggestion.UserId = uid;
            _db.TblUserSuggestions.Add(tblUserSuggestion);
            _db.SaveChanges();

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel16"), "Level16"));
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