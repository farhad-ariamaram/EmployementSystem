using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level4
{
    public class AddAnotherLeaveReasonModel : PageModel
    {
        private readonly EmployDBContext _db;
        public AddAnotherLeaveReasonModel(EmployDBContext db)
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

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentJob")))
            {
                return RedirectToPage("../Index");
            }

            var q = from a in _db.TblWorkExperienceLeaveJobDtls
                    where a.FldWorkExperienceId == Int64.Parse(HttpContext.Session.GetString("CurrentJob"))
                    select a;

            if (q.AsEnumerable().Sum(o => o.FldLeaveJobPercent) >= 100)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}