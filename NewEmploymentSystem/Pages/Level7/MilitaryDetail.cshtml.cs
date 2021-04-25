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
    public class MilitaryDetailModel : PageModel
    {
        private readonly EmployDBContext _db;

        public MilitaryDetailModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.TblUserMilitary military { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");
            ViewData["MilitaryId"] = HttpContext.Session.GetString("MilitaryStatus");

            if(uid==null || ViewData["MilitaryId"]==null)
            {
                return RedirectToPage("../Index"); 
            }

            ViewData["OrganizationId"] = new SelectList(_db.TblMilitaryOrganizations, "Id", "OrganizationName");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["MilitaryId"] = HttpContext.Session.GetString("MilitaryStatus");
                ViewData["OrganizationId"] = new SelectList(_db.TblMilitaryOrganizations, "Id", "OrganizationName");
                return Page();
            }

            string uid = HttpContext.Session.GetString("uid");

            //Time Logging
            _db.TblPageTimeLogs.Add(Utility.logTime(uid, HttpContext.Session.GetString("EnterLevel7"), "Level7"));
            _db.SaveChanges();

            if (HttpContext.Session.GetString("MilitaryStatus").Equals("2"))
            {
                PersianCalendar pc = new PersianCalendar();
                string[] startDate = Request.Form["StartDate"].ToString().Split("/");
                DateTime sd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);
                string[] endDate = Request.Form["EndDate"].ToString().Split("/");
                DateTime ed = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);
                military.StartDate = sd;
                military.EndDate = ed;
            }

            military.UserId = uid;
            military.MilitaryId = int.Parse(HttpContext.Session.GetString("MilitaryStatus"));
            _db.TblUserMilitaries.Add(military);
            _db.SaveChanges();

            TblUser user1 = _db.TblUsers.Find(uid);
            string currentUserLevel1 = user1.CurrentLevel;
            string nextUserLevel1 = null;
            nextUserLevel1 = Utility.findNextPagesSequence(currentUserLevel1);
            user1.CurrentLevel = nextUserLevel1;
            _db.SaveChanges();
            return RedirectToPage("../Index");

        }
    }
}