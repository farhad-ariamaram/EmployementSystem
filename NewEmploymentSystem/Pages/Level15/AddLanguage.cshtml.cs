using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level15
{
    public class AddLanguageModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddLanguageModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblUserLanguage tblUserLanguage { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["LanguageTypeId"] = new SelectList(_db.TblLanguageTypes, "Id", "LanguageType");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["LanguageTypeId"] = new SelectList(_db.TblLanguageTypes, "Id", "LanguageType");
                return Page();
            }

            tblUserLanguage.UserId = HttpContext.Session.GetString("uid");
            _db.TblUserLanguages.Add(tblUserLanguage);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}