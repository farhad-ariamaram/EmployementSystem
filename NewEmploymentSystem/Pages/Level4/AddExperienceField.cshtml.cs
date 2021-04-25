using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewEmploymentSystem.Pages.Level4
{
    public class AddExperienceFieldModel : PageModel
    {
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
            HttpContext.Session.SetString("ExperienceTitle", Request.Form["ExperienceTitle"]);
            return RedirectToPage("AddExperienceDetail");
        }
    }
}