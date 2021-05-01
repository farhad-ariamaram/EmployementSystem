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
            tblUserLanguage.ReadingLevel = int.Parse(Request.Form["ReadingLevel"].ToString());
            tblUserLanguage.ListeningLevel = int.Parse(Request.Form["ListeningLevel"].ToString());
            tblUserLanguage.SpeakingLevel = int.Parse(Request.Form["SpeakingLevel"].ToString());
            tblUserLanguage.WritingLevel = int.Parse(Request.Form["WritingLevel"].ToString());
            _db.TblUserLanguages.Add(tblUserLanguage);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}