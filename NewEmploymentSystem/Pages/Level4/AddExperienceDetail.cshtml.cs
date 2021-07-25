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

namespace NewEmploymentSystem.Pages.Level4
{
    public class AddExperienceDetailModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddExperienceDetailModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblWorkExperience tblWorkExperience { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }
            string ExperienceTitle = HttpContext.Session.GetString("ExperienceTitle");
            ViewData["JobId"] = new SelectList(_db.TblJobTamins.Where(a => a.FldTaminJobName.Replace("ي", "ی").Replace("ك", "ک").Contains(ExperienceTitle)), "FldTaminJobId", "FldTaminJobName");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                string ExperienceTitle = HttpContext.Session.GetString("ExperienceTitle");
                ViewData["JobId"] = new SelectList(_db.TblJobTamins.Where(a => a.FldTaminJobName.Replace("ي", "ی").Replace("ك", "ک").Contains(ExperienceTitle)), "FldTaminJobId", "FldTaminJobName");
                return Page();
            }

            PersianCalendar pc = new PersianCalendar();
            string[] startDate = Request.Form["StartDate"].ToString().Split("/");
            DateTime sd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);
            string[] endDate = Request.Form["EndDate"].ToString().Split("/");
            DateTime ed = new DateTime(int.Parse(endDate[0]), int.Parse(endDate[1]), int.Parse(endDate[2]), pc);

            tblWorkExperience.FldStartDate = sd;
            tblWorkExperience.FldEndDate = ed;
            tblWorkExperience.IsWorking = bool.Parse(HttpContext.Session.GetString("IsWorking"));
            tblWorkExperience.UserId = HttpContext.Session.GetString("uid");
            tblWorkExperience.FldJobTitle = HttpContext.Session.GetString("ExperienceTitle");
            
            _db.TblWorkExperiences.Add(tblWorkExperience);
            _db.SaveChanges();
            HttpContext.Session.SetString("CurrentJob", tblWorkExperience.FldWorkExperienceId.ToString());
            return RedirectToPage("AddWorkLeaveReasonPercent");
        }
    }
}