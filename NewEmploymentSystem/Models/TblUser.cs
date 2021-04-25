using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblCustomerDegrees = new HashSet<TblCustomerDegree>();
            TblHowFinds = new HashSet<TblHowFind>();
            TblIpLogs = new HashSet<TblIpLog>();
            TblPageTimeLogs = new HashSet<TblPageTimeLog>();
            TblPrimaryInformations = new HashSet<TblPrimaryInformation>();
            TblUserJobs = new HashSet<TblUserJob>();
            TblUserMilitaries = new HashSet<TblUserMilitary>();
            TblUserSkills = new HashSet<TblUserSkill>();
            TblWorkExperiences = new HashSet<TblWorkExperience>();
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CurrentLevel { get; set; }
        public long? PagesSequenceId { get; set; }
        public string PostalCode { get; set; }
        public string TrackNo { get; set; }

        public virtual TblPagesSequence PagesSequence { get; set; }
        public virtual ICollection<TblCustomerDegree> TblCustomerDegrees { get; set; }
        public virtual ICollection<TblHowFind> TblHowFinds { get; set; }
        public virtual ICollection<TblIpLog> TblIpLogs { get; set; }
        public virtual ICollection<TblPageTimeLog> TblPageTimeLogs { get; set; }
        public virtual ICollection<TblPrimaryInformation> TblPrimaryInformations { get; set; }
        public virtual ICollection<TblUserJob> TblUserJobs { get; set; }
        public virtual ICollection<TblUserMilitary> TblUserMilitaries { get; set; }
        public virtual ICollection<TblUserSkill> TblUserSkills { get; set; }
        public virtual ICollection<TblWorkExperience> TblWorkExperiences { get; set; }
    }
}
