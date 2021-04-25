﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level3
{
    public class PostDiplomaFieldModel : PageModel
    {
        private readonly EmployDBContext _db;

        public PostDiplomaFieldModel(EmployDBContext db)
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
                    where a.UserId == uid && a.DiplomaId == 11
                    select a;

            if (q.Any())
            {
                return RedirectToPage("DiplomaField");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("PostDiplomReshteh", Request.Form["PostDiplomReshteh"]);
            return RedirectToPage("PostDiplomaDetail");
        }
    }
}