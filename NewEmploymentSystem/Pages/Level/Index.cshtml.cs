using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewEmploymentSystem.EmployReqModels;
using NewEmploymentSystem.Models;

namespace NewEmploymentSystem.Pages.Level
{
    public class IndexModel : PageModel
    {
        private readonly EmployDBContext _db;
        private readonly EmployeeRequestDBContext _db2;

        public IndexModel(EmployDBContext db, EmployeeRequestDBContext db2)
        {
            _db = db;
            _db2 = db2;
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





            var t = _db2.TblEmployeeRequestEmployees.Find(uid);
            if (t == null)
            {
                TblUser TblUser = _db.TblUsers.Find(uid);
                EmployReqModels.TblEmployeeRequestEmployee TblEmployeeRequestEmployee = new TblEmployeeRequestEmployee()
                {
                    FldEmployeeRequestEmployeeId = TblUser.Id,
                    FldEmployeeRequestEmployeeCurrentLevel = TblUser.CurrentLevel,
                    FldEmployeeRequestEmployeePassword = TblUser.Password,
                    FldEmployeeRequestEmployeeUsername = TblUser.Username,
                    FldEmployeeRequestPagesSequenceId = TblUser.PagesSequenceId
                };
                _db2.TblEmployeeRequestEmployees.Add(TblEmployeeRequestEmployee);


                var TblEmergencyCalls = _db.TblEmergencyCalls.Where(a => a.UserId == uid);
                if (TblEmergencyCalls.Any())
                {
                    foreach (var item in TblEmergencyCalls)
                    {
                        EmployReqModels.TblEmployeeRequestEmergencyCall tblEmployeeRequestEmergencyCall = new TblEmployeeRequestEmergencyCall()
                        {
                            FldEmployeeRequestEmergencyCallDescription = item.Description,
                            FldEmployeeRequestEmergencyCallFirstName = item.FirstName,
                            FldEmployeeRequestEmergencyCallId = item.Id,
                            FldEmployeeRequestEmergencyCallLastName = item.LastName,
                            FldEmployeeRequestEmergencyCallPhoneNo = item.PhoneNo,
                            FldEmployeeRequestEmergencyCallRelative = item.Relative,
                            FldEmployeeRequestEmployeeId = item.UserId
                        };
                        _db2.TblEmployeeRequestEmergencyCalls.Add(tblEmployeeRequestEmergencyCall);
                    }
                }


                var TblCustomerDegrees = _db.TblCustomerDegrees.Where(a => a.UserId == uid);
                if (TblCustomerDegrees.Any())
                {
                    foreach (var item in TblCustomerDegrees)
                    {
                        EmployReqModels.TblCustomerDegree TblCustomerDegree = new EmployReqModels.TblCustomerDegree()
                        {
                            UserId = item.UserId,
                            DiplomaId = item.DiplomaId,
                            EducationId = item.EducationId,
                            FldCustomerDegreeId = item.FldCustomerDegreeId,
                            FldDes = item.FldDes,
                            FldEducationName = item.FldEducationName,
                            FldEndDate = item.FldEndDate,
                            FldExportDate = item.FldExportDate,
                            FldExportNo = item.FldExportNo,
                            FldPoint = item.FldPoint,
                            FldStartDate = item.FldStartDate,
                            FldStudyCity = item.FldStudyCity,
                            FldStudyPlace = item.FldStudyPlace
                        };
                        _db2.TblCustomerDegrees.Add(TblCustomerDegree);
                    }
                }


                var TblGeneralRecords = _db.TblGeneralRecords.Where(a => a.UserId == uid);
                if (TblGeneralRecords.Any())
                {
                    foreach (var item in TblGeneralRecords)
                    {
                        EmployReqModels.TblEmployeeRequestGeneralRecord TblEmployeeRequestGeneralRecord = new TblEmployeeRequestGeneralRecord()
                        {
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestGeneralRecordCriminalDes = item.CriminalDes,
                            FldEmployeeRequestGeneralRecordCriminalTiltle = item.CriminalTiltle,
                            FldEmployeeRequestGeneralRecordDescription = item.Description,
                            FldEmployeeRequestGeneralRecordId = item.Id
                        };
                        _db2.TblEmployeeRequestGeneralRecords.Add(TblEmployeeRequestGeneralRecord);
                    }
                }


                TblHowFind TblHowFind = _db.TblHowFinds.Where(a => a.UserId == uid).FirstOrDefault();
                if (TblHowFind != null)
                {
                    EmployReqModels.TblEmployeeRequestHowFind TblEmployeeRequestHowFind = new TblEmployeeRequestHowFind()
                    {
                        FldEmployeeRequestEmployeeId = TblHowFind.UserId,
                        FldEmployeeRequestHowFindAdditionalDes = TblHowFind.AdditionalDes,
                        FldEmployeeRequestHowFindId = TblHowFind.Id,
                        FldEmployeeRequestHowFindTitle = TblHowFind.HowFindTitle
                    };
                    _db2.TblEmployeeRequestHowFinds.Add(TblEmployeeRequestHowFind);
                }



                var TblIpLogs = _db.TblIpLogs.Where(a => a.UserId == uid);
                if (TblIpLogs.Any())
                {
                    foreach (var item in TblIpLogs)
                    {
                        EmployReqModels.TblEmployeeRequestIpLog TblEmployeeRequestIpLog = new TblEmployeeRequestIpLog()
                        {
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestIpLogDateTime = item.DateTime,
                            FldEmployeeRequestIpLogId = item.Id,
                            FldEmployeeRequestIpLogIp = item.Ip
                        };
                        _db2.TblEmployeeRequestIpLogs.Add(TblEmployeeRequestIpLog);
                    }
                }


                var TblMedicalRecords = _db.TblMedicalRecords.Where(a => a.UserId == uid);
                if (TblMedicalRecords.Any())
                {
                    foreach (var item in TblMedicalRecords)
                    {
                        EmployReqModels.TblEmployeeRequestMedicalRecord TblEmployeeRequestMedicalRecord = new TblEmployeeRequestMedicalRecord()
                        {
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestMedicalRecordComplicationsDes = item.ComplicationsDes,
                            FldEmployeeRequestMedicalRecordDescription = item.Description,
                            FldEmployeeRequestMedicalRecordDisease = item.Disease,
                            FldEmployeeRequestMedicalRecordEndDate = item.EndDate,
                            FldEmployeeRequestMedicalRecordHasComplications = item.HasComplications,
                            FldEmployeeRequestMedicalRecordHasProblem = item.HasProblem,
                            FldEmployeeRequestMedicalRecordId = item.Id,
                            FldEmployeeRequestMedicalRecordIsAddict = item.IsAddict,
                            FldEmployeeRequestMedicalRecordProblemDes = item.ProblemDes,
                            FldEmployeeRequestMedicalRecordStartDate = item.StartDate
                        };
                        _db2.TblEmployeeRequestMedicalRecords.Add(TblEmployeeRequestMedicalRecord);
                    }
                }


                var TblPageTimeLogs = _db.TblPageTimeLogs.Where(a => a.UserId == uid);
                if (TblPageTimeLogs.Any())
                {
                    foreach (var item in TblPageTimeLogs)
                    {
                        EmployReqModels.TblEmployeeRequestPageTimeLog TblEmployeeRequestPageTimeLog = new TblEmployeeRequestPageTimeLog()
                        {
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestPageTimeLogEndTime = item.EndTime,
                            FldEmployeeRequestPageTimeLogStartTime = item.StartTime,
                            FldEmployeeRequestPageTimeLogId = item.Id,
                            FldEmployeeRequestPageTimeLogPageLevel = item.PageLevel
                        };
                        _db2.TblEmployeeRequestPageTimeLogs.Add(TblEmployeeRequestPageTimeLog);
                    }
                }



                TblPrimaryInformation TblPrimaryInformation = _db.TblPrimaryInformations.Where(a => a.UserId == uid).FirstOrDefault();
                if (TblPrimaryInformation != null)
                {
                    EmployReqModels.TblEmployeeRequestPrimaryInformation TblEmployeeRequestPrimaryInformation = new TblEmployeeRequestPrimaryInformation()
                    {
                        FldEmployeeRequestEmployeeId = TblPrimaryInformation.UserId,
                        FldEmployeeRequestPrimaryInformationBirthDate = TblPrimaryInformation.BirthDate,
                        FldEmployeeRequestPrimaryInformationChildrenNo = TblPrimaryInformation.ChildrenNo,
                        FldEmployeeRequestPrimaryInformationFirstName = TblPrimaryInformation.FirstName,
                        FldEmployeeRequestPrimaryInformationGender = TblPrimaryInformation.Gender,
                        FldEmployeeRequestPrimaryInformationId = TblPrimaryInformation.Id,
                        FldEmployeeRequestPrimaryInformationLastName = TblPrimaryInformation.LastName,
                        FldEmployeeRequestPrimaryInformationMarital = TblPrimaryInformation.Marital,
                        FldEmployeeRequestPrimaryInformationNationalCode = TblPrimaryInformation.NationalCode,
                        FldEmployeeRequestPrimaryInformationPhoneNo = TblPrimaryInformation.PhoneNo,
                        FldEmployeeRequestPrimaryInformationPostalCode = TblPrimaryInformation.PostalCode,
                        FldEmployeeRequestPrimaryInformationTrackNo = TblPrimaryInformation.TrackNo,
                        FldEmployeeRequestPrimaryInformationTutelage = TblPrimaryInformation.Tutelage
                    };
                    _db2.TblEmployeeRequestPrimaryInformations.Add(TblEmployeeRequestPrimaryInformation);
                }



                var TblUserCompilations = _db.TblUserCompilations.Where(a => a.UserId == uid);
                if (TblUserCompilations.Any())
                {
                    foreach (var item in TblUserCompilations)
                    {
                        EmployReqModels.TblEmployeeRequestUserCompilation TblEmployeeRequestUserCompilation = new TblEmployeeRequestUserCompilation()
                        {
                            FldEmployeeRequestCompilationTypeId = item.CompilationTypeId,
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestUserCompilationAuthor = item.Author,
                            FldEmployeeRequestUserCompilationDate = item.Date,
                            FldEmployeeRequestUserCompilationDescription = item.Description,
                            FldEmployeeRequestUserCompilationExplanation = item.Explanation,
                            FldEmployeeRequestUserCompilationId = item.Id,
                            FldEmployeeRequestUserCompilationTitle = item.Title
                        };
                        _db2.TblEmployeeRequestUserCompilations.Add(TblEmployeeRequestUserCompilation);
                    }
                }



                var TblUserCreativitys = _db.TblUserCreativities.Where(a => a.UserId == uid);
                if (TblUserCreativitys.Any())
                {
                    foreach (var item in TblUserCreativitys)
                    {
                        EmployReqModels.TblEmployeeRequestUserCreativity TblEmployeeRequestUserCreativity = new TblEmployeeRequestUserCreativity()
                        {
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestCreativityTypeId = item.CreativityTypeId,
                            FldEmployeeRequestUserCreativityDate = item.Date,
                            FldEmployeeRequestUserCreativityDescription = item.Description,
                            FldEmployeeRequestUserCreativityExplanation = item.Explanation,
                            FldEmployeeRequestUserCreativityId = item.Id,
                            FldEmployeeRequestUserCreativityTitle = item.Title
                        };
                        _db2.TblEmployeeRequestUserCreativities.Add(TblEmployeeRequestUserCreativity);
                    }
                }



                var TblUserJobs = _db.TblUserJobs.Where(a => a.UserId == uid);
                if (TblUserJobs.Any())
                {
                    foreach (var item in TblUserJobs)
                    {
                        EmployReqModels.TblEmployeeRequestUserJob TblEmployeeRequestUserJob = new TblEmployeeRequestUserJob()
                        {
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestJobsId = item.JobsId,
                            FldEmployeeRequestUserJobDescription = item.Description,
                            FldEmployeeRequestUserJobId = item.Id,
                            FldEmployeeRequestUserJobRequestMoney = item.RequestMoney,
                            FldEmployeeRequestUserJobTitle = item.JobTitle,
                            FldEmployeeRequestUserJobWhatKnowAbout = item.WhatKnowAbout
                        };
                        _db2.TblEmployeeRequestUserJobs.Add(TblEmployeeRequestUserJob);
                    }
                }



                var TblUserLanguages = _db.TblUserLanguages.Where(a => a.UserId == uid);
                if (TblUserLanguages.Any())
                {
                    foreach (var item in TblUserLanguages)
                    {
                        EmployReqModels.TblEmployeeRequestUserLanguage TblEmployeeRequestUserLanguage = new TblEmployeeRequestUserLanguage()
                        {
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestUserLanguageDescription = item.Description,
                            FldEmployeeRequestUserLanguageId = item.Id,
                            FldEmployeeRequestUserLanguageLanguageTypeId = item.LanguageTypeId,
                            FldEmployeeRequestUserLanguageListeningLevel = item.ListeningLevel,
                            FldEmployeeRequestUserLanguageReadingLevel = item.ReadingLevel,
                            FldEmployeeRequestUserLanguageSpeakingLevel = item.SpeakingLevel,
                            FldEmployeeRequestUserLanguageWritingLevel = item.WritingLevel,
                        };
                        _db2.TblEmployeeRequestUserLanguages.Add(TblEmployeeRequestUserLanguage);
                    }
                }



                TblUserMilitary TblUserMilitary = _db.TblUserMilitaries.Where(a => a.UserId == uid).FirstOrDefault();
                if (TblUserMilitary != null)
                {
                    if (TblUserMilitary != null)
                    {
                        EmployReqModels.TblEmployeeRequestUserMilitary TblEmployeeRequestUserMilitary = new TblEmployeeRequestUserMilitary()
                        {
                            FldEmployeeRequestEmployeeId = TblUserMilitary.UserId,
                            FldEmployeeRequestMilitaryId = TblUserMilitary.MilitaryId,
                            FldEmployeeRequestMilitaryOrganizationId = TblUserMilitary.OrganizationId,
                            FldEmployeeRequestUserMilitaryCity = TblUserMilitary.City,
                            FldEmployeeRequestUserMilitaryDescription = TblUserMilitary.Description,
                            FldEmployeeRequestUserMilitaryEndDate = TblUserMilitary.EndDate,
                            FldEmployeeRequestUserMilitaryExemptDescription = TblUserMilitary.ExemptDescription,
                            FldEmployeeRequestUserMilitaryId = TblUserMilitary.Id,
                            FldEmployeeRequestUserMilitaryStartDate = TblUserMilitary.StartDate,
                            FldEmployeeRequestUserMilitaryUnit = TblUserMilitary.Unit
                        };
                        _db2.TblEmployeeRequestUserMilitaries.Add(TblEmployeeRequestUserMilitary);
                    }
                }




                var TblUserSkills = _db.TblUserSkills.Where(a => a.UserId == uid);
                if (TblUserSkills.Any())
                {
                    foreach (var item in TblUserSkills)
                    {
                        EmployReqModels.TblEmployeeRequestUserSkill TblEmployeeRequestUserSkill = new TblEmployeeRequestUserSkill()
                        {
                            FldEmployeeRequestEmployeeId = item.UserId,
                            FldEmployeeRequestSkillsId = item.SkillId,
                            FldEmployeeRequestUserSkillDate = item.Date,
                            FldEmployeeRequestUserSkillDescription = item.Description,
                            FldEmployeeRequestUserSkillId = item.Id,
                            FldEmployeeRequestUserSkillIsSelfTaught = item.IsSelfTaught,
                            FldEmployeeRequestUserSkillLicenseNo = item.LicenseNo,
                            FldEmployeeRequestUserSkillLicenseReference = item.LicenseReference,
                            FldEmployeeRequestUserSkillLocation = item.Location,
                            FldEmployeeRequestUserSkillSkillLevel = item.SkillLevel,
                            FldEmployeeRequestUserSkillSkillTitle = item.SkillTitle,
                            FldEmployeeRequestUserSkillSkillType = item.SkillType
                        };
                        _db2.TblEmployeeRequestUserSkills.Add(TblEmployeeRequestUserSkill);
                    }
                }

                List<long> weids = new List<long>();
                var TblWorkExperiences = _db.TblWorkExperiences.Where(a => a.UserId == uid);
                if (TblWorkExperiences.Any())
                {
                    foreach (var item in TblWorkExperiences)
                    {
                        weids.Add(item.FldWorkExperienceId);
                        EmployReqModels.TblWorkExperience TblWorkExperience = new EmployReqModels.TblWorkExperience()
                        {
                            FldAmountOfDailyInsurance = item.FldAmountOfDailyInsurance,
                            FldCompanyName = item.FldCompanyName,
                            FldContactInnerNumberOfCompany = item.FldContactInnerNumberOfCompany,
                            FldContactNumberOfCompany = item.FldContactNumberOfCompany,
                            FldCustomerName = item.FldCustomerName,
                            FldDescription = item.FldDescription,
                            FldEarlySalary = item.FldEarlySalary,
                            FldEndDate = item.FldEndDate,
                            FldJobTitle = item.FldJobTitle,
                            FldLateSalary = item.FldLateSalary,
                            FldLeaveJobId = item.FldLeaveJobId,
                            FldReasonsToLeaveJob = item.FldReasonsToLeaveJob,
                            FldRelatedPeople = item.FldRelatedPeople,
                            FldStartDate = item.FldStartDate,
                            FldTaminJobId = item.FldTaminJobId,
                            FldUnitName = item.FldUnitName,
                            FldWorkDay = item.FldWorkDay,
                            FldWorkExperienceId = item.FldWorkExperienceId,
                            FldWorkTime = item.FldWorkTime,
                            HasInsurance = item.HasInsurance,
                            InsuranceNo = item.InsuranceNo,
                            IsWorking = item.IsWorking,
                            PreviousJobAchievements = item.PreviousJobAchievements,
                            UserId = item.UserId,
                            WhyWantChangeJob = item.WhyWantChangeJob,
                        };
                        _db2.TblWorkExperiences.Add(TblWorkExperience);
                    }
                }

                if (weids.Any())
                {
                    foreach (var item in weids)
                    {
                        var TblWorkExperienceLeaveJobDtls = _db.TblWorkExperienceLeaveJobDtls.Where(a => a.FldWorkExperienceId == item);
                        if (TblWorkExperienceLeaveJobDtls.Any())
                        {
                            foreach (var item2 in TblWorkExperienceLeaveJobDtls)
                            {
                                EmployReqModels.TblWorkExperienceLeaveJobDtl TblWorkExperienceLeaveJobDtl = new EmployReqModels.TblWorkExperienceLeaveJobDtl()
                                {
                                    FldLeaveJobId = item2.FldLeaveJobId,
                                    FldLeaveJobPercent = item2.FldLeaveJobPercent,
                                    FldWorkExperienceId = item2.FldWorkExperienceId,
                                    FldWorkExperienceLeaveJobDtlId = item2.FldWorkExperienceLeaveJobDtlId,
                                    FldLeaveJob = item2.FldLeaveJob
                                };
                                _db2.TblWorkExperienceLeaveJobDtls.Add(TblWorkExperienceLeaveJobDtl);
                            }
                        }
                    }
                }


                _db2.SaveChanges();
            }



            return Page();
        }
    }
}