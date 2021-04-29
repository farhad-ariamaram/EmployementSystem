using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;
using NewEmploymentSystem.Utilities;

namespace NewEmploymentSystem.Pages.Level10
{
    public class AddAddictionRecordModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddAddictionRecordModel(EmployDBContext db)
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
            string[] startDate = Request.Form["StartDate"].ToString().Split("/");
            string[] EndDate = Request.Form["EndDate"].ToString().Split("/");

            DateTime sd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);
            DateTime ed = DateTime.MinValue;
            if (!string.IsNullOrEmpty(EndDate[0]))
            {
                ed = new DateTime(int.Parse(EndDate[0]), int.Parse(EndDate[1]), int.Parse(EndDate[2]), pc);
            }

            tblMedicalRecord.StartDate = sd;
            if (!string.IsNullOrEmpty(EndDate[0]))
            {
                tblMedicalRecord.EndDate = ed;
            }
            tblMedicalRecord.IsAddict = true;
            tblMedicalRecord.HasProblem = false;
            tblMedicalRecord.UserId = HttpContext.Session.GetString("uid");
            _db.TblMedicalRecords.Add(tblMedicalRecord);
            _db.SaveChanges();

            return RedirectToPage("../Index");
        }
    }
}