using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages.Level3
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
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["Level"] = Utility.leveltoNumber(_db.TblUsers.Find(uid).CurrentLevel);

            var q = from a in _db.TblCustomerDegrees
                    where a.UserId == uid
                    select a;

            if (q.Any())
            {
                TblUser user = _db.TblUsers.Find(uid);
                string currentUserLevel = user.CurrentLevel;
                string nextUserLevel = null;
                nextUserLevel = Utility.findNextPagesSequence(currentUserLevel);
                user.CurrentLevel = nextUserLevel;
                _db.SaveChanges();
                return RedirectToPage("../Index");
            }

            ViewData["DiplomaId"] = new SelectList(_db.PayDiplomas, "DiplomaId", "DiplomaName");

            //Time Logging
            if (HttpContext.Session.GetString("EnterLevel3") == null)
            {
                HttpContext.Session.SetString("EnterLevel3", DateTime.Now.ToString());
            }
            
            return Page();
        }

        [BindProperty]
        public TblCustomerDegree tblCustomerDegree { get; set; }

        public IActionResult OnPost()
        {
            string uid = HttpContext.Session.GetString("uid");

            //بی سواد
            if (tblCustomerDegree.DiplomaId == 1)
            {
                tblCustomerDegree.UserId = uid;
                tblCustomerDegree.DiplomaId = 1;
                _db.TblCustomerDegrees.Add(tblCustomerDegree);
                _db.SaveChanges();

                //Time Logging
                _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel3"), "Level3"));
                _db.SaveChanges();

                return RedirectToPage("../Index");
            }


            //زیردیپلم
            if (tblCustomerDegree.DiplomaId == 4)
            {
                tblCustomerDegree.UserId = uid;
                tblCustomerDegree.DiplomaId = 4;
                _db.TblCustomerDegrees.Add(tblCustomerDegree);
                _db.SaveChanges();

                //Time Logging
                _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel3"), "Level3"));
                _db.SaveChanges();

                return RedirectToPage("../Index");
            }

            //دیپلم
            if (tblCustomerDegree.DiplomaId == 5)
            {
                return RedirectToPage("DiplomaField");
            }

            //فوق دیپلم
            if (tblCustomerDegree.DiplomaId == 11)
            {
                return RedirectToPage("PostDiplomaField");
            }

            //کارشناسی
            if (tblCustomerDegree.DiplomaId == 12)
            {
                return RedirectToPage("BchField");
            }

            //کارشناسی ارشد
            if (tblCustomerDegree.DiplomaId == 13)
            {
                return RedirectToPage("MasterField");
            }

            //دکتری
            if (tblCustomerDegree.DiplomaId == 17)
            {
                return RedirectToPage("PhdField");
            }

            return RedirectToPage("../Index");

        }
    }
}