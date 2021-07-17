using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewEmploymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : Controller
    {
        private EmployDBContext _db;

        public PersonController(EmployDBContext db)
        {
            _db = db;
        }

        [HttpGet("get/{user}/{pass}")]
        public async Task<ActionResult<IEnumerable<TblUser>>> GetPeople(string user, string pass)
        {
            if(user=="ym" && pass == "S33@||")
            {
                var notTransferedUsers = await _db.TblUsers
                                .Include(a => a.TblCustomerDegrees)
                                .Include(a => a.TblEmergencyCalls)
                                .Include(a => a.TblGeneralRecords)
                                .Include(a => a.TblHowFinds)
                                .Include(a => a.TblIpLogs)
                                .Include(a => a.TblMedicalRecords)
                                .Include(a => a.TblPageTimeLogs)
                                .Include(a => a.TblPrimaryInformations)
                                .Include(a => a.TblUserCompilations)
                                .Include(a => a.TblUserCreativities)
                                .Include(a => a.TblUserJobs)
                                .Include(a => a.TblUserLanguages)
                                .Include(a => a.TblUserMilitaries)
                                .Include(a => a.TblUserSkills)
                                .Include(a => a.TblWorkExperiences)
                                .Where(a => a.IsTransfered == false && a.TblPrimaryInformations.FirstOrDefault().TrackNo != null)
                                .ToListAsync();

                return notTransferedUsers;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var user = await _db.TblUsers.FindAsync(id);
            if (user != null)
            {
                user.IsTransfered = true;
                _db.TblUsers.Update(user);
                await _db.SaveChangesAsync();
                return new JsonResult("Ok");
            }
            return new JsonResult("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> PostPerson(TblJob job)
        {
            try
            {
                TblJob newJob = new TblJob()
                {
                    JobTitle = job.JobTitle,
                    IsActive = job.IsActive,
                    StartDate = job.StartDate,
                    EndDate = job.EndDate,
                    NeedMan = job.NeedMan,
                    NeedWoman = job.NeedWoman,
                    Description = job.Description
                };
                await _db.TblJobs.AddAsync(newJob);
                await _db.SaveChangesAsync();

                return new JsonResult(newJob.Id);
            }
            catch (Exception)
            {
                return new JsonResult("Error");
            }
        }
    }
}
