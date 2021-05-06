using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<TblUserLanguage> listUserLanguages { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            listUserLanguages = _db.TblUserLanguages.Where(a => a.UserId.Equals(uid));

            ViewData["LanguageTypeId"] = new SelectList(_db.TblLanguageTypes.Where(p => !(listUserLanguages.Select(a => a.LanguageTypeId)).Contains(p.Id)), "Id", "LanguageType");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                string uid = HttpContext.Session.GetString("uid");
                listUserLanguages = _db.TblUserLanguages.Where(a => a.UserId.Equals(uid));
                ViewData["LanguageTypeId"] = new SelectList(_db.TblLanguageTypes.Where(p => !(listUserLanguages.Select(a => a.LanguageTypeId)).Contains(p.Id)), "Id", "LanguageType");
                return Page();
            }

            tblUserLanguage.UserId = HttpContext.Session.GetString("uid");
            if (!string.IsNullOrEmpty(Request.Form["ReadingLevel"].ToString()))
            {
                tblUserLanguage.ReadingLevel = int.Parse(Request.Form["ReadingLevel"].ToString());
                tblUserLanguage.ListeningLevel = int.Parse(Request.Form["ListeningLevel"].ToString());
                tblUserLanguage.SpeakingLevel = int.Parse(Request.Form["SpeakingLevel"].ToString());
                tblUserLanguage.WritingLevel = int.Parse(Request.Form["WritingLevel"].ToString());
            }
            _db.TblUserLanguages.Add(tblUserLanguage);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}