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

namespace NewEmploymentSystem.Pages.Level13
{
    public class AddCreativityModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddCreativityModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblUserCreativity tblUserCreativity { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["CreativityTypeId"] = new SelectList(_db.TblCreativityTypes, "Id", "CreativityType");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CreativityTypeId"] = new SelectList(_db.TblCreativityTypes, "Id", "CreativityType");
                return Page();
            }

            PersianCalendar pc = new PersianCalendar();
            string[] startDate = Request.Form["GetDate"].ToString().Split("/");
            DateTime gd = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), pc);

            tblUserCreativity.Date = gd;
            tblUserCreativity.UserId = HttpContext.Session.GetString("uid");
            _db.TblUserCreativities.Add(tblUserCreativity);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}