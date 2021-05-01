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

namespace NewEmploymentSystem.Pages.Level14
{
    public class AddCompilationModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddCompilationModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblUserCompilation tblUserCompilation { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["CompilationTypeId"] = new SelectList(_db.TblCompilationTypes, "Id", "CompilationType");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CompilationTypeId"] = new SelectList(_db.TblCompilationTypes, "Id", "CompilationType");
                return Page();
            }

            PersianCalendar pc = new PersianCalendar();
            string[] startDate = Request.Form["GetDate"].ToString().Split("/");
            DateTime gd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);

            tblUserCompilation.Date = gd;
            tblUserCompilation.UserId = HttpContext.Session.GetString("uid");
            _db.TblUserCompilations.Add(tblUserCompilation);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}