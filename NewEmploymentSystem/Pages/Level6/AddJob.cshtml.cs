using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level6
{
    public class AddJobModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddJobModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblUserJob userJob { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            ViewData["JobId"] = new SelectList(_db.TblJobs.Where(a => a.IsActive == true), "Id", "JobTitle");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["JobId"] = new SelectList(_db.TblJobs, "Id", "JobTitle");
                return Page();
            }
            userJob.UserId = HttpContext.Session.GetString("uid");
            _db.TblUserJobs.Add(userJob);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}