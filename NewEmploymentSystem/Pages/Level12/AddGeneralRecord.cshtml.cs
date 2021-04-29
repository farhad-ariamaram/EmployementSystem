using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level12
{
    public class AddGeneralRecordModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddGeneralRecordModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblGeneralRecord tblGeneralRecord { get; set; }

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

            tblGeneralRecord.UserId = HttpContext.Session.GetString("uid");
            _db.TblGeneralRecords.Add(tblGeneralRecord);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}