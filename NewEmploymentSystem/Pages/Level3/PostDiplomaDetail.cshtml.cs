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

namespace NewEmploymentSystem.Pages.Level3
{
    public class PostDiplomaDetailModel : PageModel
    {
        private readonly EmployDBContext _db;

        public PostDiplomaDetailModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblCustomerDegree tblCustomerDegree { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            var q = from a in _db.TblCustomerDegrees
                    where a.UserId == uid && a.DiplomaId == 11
                    select a;

            if (q.Any())
            {
                return RedirectToPage("DiplomaField");
            }

            string PostDiplomReshteh = HttpContext.Session.GetString("PostDiplomReshteh");

            ViewData["EducationId"] = new SelectList(_db.PayEducations.Where(a => a.EducationName.Replace("ي", "ی").Replace("ك", "ک").Contains(PostDiplomReshteh)), "EducationId", "EducationName");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {

                string PostDiplomReshteh = HttpContext.Session.GetString("PostDiplomReshteh");

                ViewData["EducationId"] = new SelectList(_db.PayEducations.Where(a => a.EducationName.Replace("ي", "ی").Replace("ك", "ک").Contains(PostDiplomReshteh)), "EducationId", "EducationName");

                return Page();
            }
            PersianCalendar pc = new PersianCalendar();
            string[] startDate = Request.Form["StartDate"].ToString().Split("/");
            DateTime sd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);
            string[] endDate = Request.Form["EndDate"].ToString().Split("/");
            DateTime ed = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);
            string[] exportDate = Request.Form["ExportDate"].ToString().Split("/");
            DateTime exd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);

            tblCustomerDegree.FldStartDate = sd;
            tblCustomerDegree.FldEndDate = ed;
            tblCustomerDegree.FldExportDate = exd;
            tblCustomerDegree.CustomerName = HttpContext.Session.GetString("Fullname");
            tblCustomerDegree.UserId = HttpContext.Session.GetString("uid");
            tblCustomerDegree.DiplomaId = 11;
            tblCustomerDegree.FldEducationName = HttpContext.Session.GetString("PostDiplomReshteh");
            _db.TblCustomerDegrees.Add(tblCustomerDegree);
            _db.SaveChanges();
            return RedirectToPage("DiplomaField");
        }
    }
}