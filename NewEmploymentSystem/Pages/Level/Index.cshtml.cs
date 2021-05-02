using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;

        public IndexModel(EmployDBContext db)
        {
            _db = db;
        }

        public string Trackcode { get; set; }

        public IActionResult OnGet()
        {
            string uid = HttpContext.Session.GetString("uid");

            //if uid session was not set redirect to "Login" page
            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToPage("../Index");
            }

            //get user track id for display
            Trackcode = _db.TblPrimaryInformations.Where(a => a.UserId.Equals(uid)).FirstOrDefault().TrackNo;

            //بروزرسانی رکورد سیستم جاری در صورت بهبود
            var q = (from a in _db.TblPageTimeLogs
                     where a.UserId == uid
                     select new
                     {
                         t = EF.Functions.DateDiffMillisecond(a.StartTime, a.EndTime)
                     }).Sum(x => x.t);

            var PreviousRecord = _db.TblPagesSequences.Find(_db.TblUsers.Find(uid).PagesSequenceId).Record;
            if (PreviousRecord == null)
            {
                PreviousRecord = long.MaxValue;
            }

            if (q < PreviousRecord)
            {
                var c = from b in _db.TblPagesSequences
                        where b.Id == _db.TblUsers.Find(uid).PagesSequenceId
                        select b;
                c.FirstOrDefault().Record = (long)q;
                _db.SaveChanges();
            }

            return Page();
        }
    }
}