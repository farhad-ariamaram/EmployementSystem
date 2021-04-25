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
    public class DiplomaFieldModel : PageModel
    {
        private readonly EmployDBContext _db;

        public DiplomaFieldModel(EmployDBContext db)
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

            var q = from a in _db.TblCustomerDegrees
                    where a.UserId == uid && a.DiplomaId == 5
                    select a;

            if (q.Any())
            {
                return RedirectToPage("../Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("ReshtehDiplom", Request.Form["ReshtehDiplom"]);
            return RedirectToPage("DiplomaDetail");
        }
    }
}