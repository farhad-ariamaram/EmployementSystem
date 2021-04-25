using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level4
{
    public class AddWorkLeaveReasonPercentModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddWorkLeaveReasonPercentModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblWorkExperienceLeaveJobDtl tblWorkExperienceLeaveJobDtl { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentJob")))
            {
                return RedirectToPage("../Index");
            }

            var q = from a in _db.TblWorkExperienceLeaveJobDtls
                    where a.FldWorkExperienceId == Int64.Parse(HttpContext.Session.GetString("CurrentJob"))
                    select a;

            ViewData["percent"] = 100 - q.AsEnumerable().Sum(o => o.FldLeaveJobPercent);

            if (q.AsEnumerable().Sum(o => o.FldLeaveJobPercent) >= 100)
            {
                return RedirectToPage("Index");
            }

            ViewData["LeaveJobId"] = new SelectList(_db.TblLeaveJobs, "FldLeaveJobId", "FldLeaveJobTitle");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["LeaveJobId"] = new SelectList(_db.TblLeaveJobs, "FldLeaveJobId", "FldLeaveJobTitle");
                return Page();
            }
            tblWorkExperienceLeaveJobDtl.FldWorkExperienceId = Int64.Parse(HttpContext.Session.GetString("CurrentJob"));
            _db.TblWorkExperienceLeaveJobDtls.Add(tblWorkExperienceLeaveJobDtl);
            _db.SaveChanges();
            return RedirectToPage("AddAnotherLeaveReason");

        }
    }
}