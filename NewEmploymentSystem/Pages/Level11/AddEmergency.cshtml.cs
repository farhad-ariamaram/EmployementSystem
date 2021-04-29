using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level11
{
    public class AddEmergencyModel : PageModel
    {
        private readonly EmployDBContext _db;

        public AddEmergencyModel(EmployDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TblEmergencyCall tblEmergencyCall { get; set; }

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

            tblEmergencyCall.UserId = HttpContext.Session.GetString("uid"); 
            _db.TblEmergencyCalls.Add(tblEmergencyCall);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}