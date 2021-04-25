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

namespace NewEmploymentSystem.Pages.Level5
{
    public class AddSkillsModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddSkillsModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblUserSkill tblUserSkill { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }
            string SkillTitle = HttpContext.Session.GetString("SkillTitle");
            ViewData["SkillId"] = new SelectList(_db.TblSkills.Where(a=>a.SkillTitle.Replace("ي", "ی").Replace("ك", "ک").Contains(SkillTitle)), "Id", "SkillTitle");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                string SkillTitle = HttpContext.Session.GetString("SkillTitle");
                ViewData["SkillId"] = new SelectList(_db.TblSkills.Where(a => a.SkillTitle.Replace("ي", "ی").Replace("ك", "ک").Contains(SkillTitle)), "Id", "SkillTitle");
                return Page();
            }

            PersianCalendar pc = new PersianCalendar();
            string[] startDate = Request.Form["GetDate"].ToString().Split("/");
            DateTime gd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);

            tblUserSkill.Date = gd;
            tblUserSkill.SkillLevel = int.Parse(Request.Form["SkillLevel"].ToString());
            tblUserSkill.UserId = HttpContext.Session.GetString("uid");
            _db.TblUserSkills.Add(tblUserSkill);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}