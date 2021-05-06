using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level10
{
    public class AddMedicalRecordModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddMedicalRecordModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblMedicalRecord tblMedicalRecord { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PersianCalendar pc = new PersianCalendar();
            if (!string.IsNullOrEmpty(Request.Form["EndDate"].ToString()))
            {
                string[] EndDate = Request.Form["EndDate"].ToString().Split("/");
                DateTime ed = new DateTime(int.Parse(EndDate[0]), int.Parse(EndDate[1]), int.Parse(EndDate[2]), pc);
                tblMedicalRecord.EndDate = ed;
            }
            string[] startDate = Request.Form["StartDate"].ToString().Split("/");
            DateTime sd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);

            tblMedicalRecord.StartDate = sd;
            tblMedicalRecord.IsAddict = false;
            tblMedicalRecord.HasProblem = true;
            tblMedicalRecord.UserId = HttpContext.Session.GetString("uid");
            _db.TblMedicalRecords.Add(tblMedicalRecord);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}