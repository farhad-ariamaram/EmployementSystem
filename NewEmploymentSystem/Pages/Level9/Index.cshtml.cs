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

namespace NewEmploymentSystem.Pages.Level9
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Postal postal { get; set; }

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
            var queryyy = (from a in _db.TblPrimaryInformations
                          where a.UserId.Equals(uid)
                          select a).FirstOrDefault();

            //check if already has track id redirect to "End"
            if (queryyy.TrackNo != null)
            {
                return RedirectToPage("../Index");
            }

            //log time
            if (HttpContext.Session.GetString("EnterLevel9") == null)
            {
                HttpContext.Session.SetString("EnterLevel9", DateTime.Now.ToString());
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
            _db.TblPrimaryInformations.Where(a=> a.UserId.Equals(uid)).FirstOrDefault().PostalCode = postal.postcode;

            //generate track id and set for user
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            while (_db.TblPrimaryInformations.Any(s => s.TrackNo == key))
            {
                key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            }
            _db.TblPrimaryInformations.Where(a => a.UserId.Equals(uid)).FirstOrDefault().TrackNo = key;
            _db.SaveChanges();

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel9"), "Level9"));
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


    //validation of postal code 
    public class Postal
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "طول این فیلد باید 10 کاراکتر ‌باشد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده صحیح نمی‌باشد")]
        public string postcode { get; set; }
    }
}