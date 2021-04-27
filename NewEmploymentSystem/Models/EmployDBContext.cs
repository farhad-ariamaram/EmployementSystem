using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NewEmploymentSystem.Models
{
    public partial class EmployDBContext : DbContext
    {
        public EmployDBContext()
        {
        }

        public EmployDBContext(DbContextOptions<EmployDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PayDiploma> PayDiplomas { get; set; }
        public virtual DbSet<PayEducation> PayEducations { get; set; }
        public virtual DbSet<TblCustomerDegree> TblCustomerDegrees { get; set; }
        public virtual DbSet<TblHowFind> TblHowFinds { get; set; }
        public virtual DbSet<TblIpLog> TblIpLogs { get; set; }
        public virtual DbSet<TblJob> TblJobs { get; set; }
        public virtual DbSet<TblJobTamin> TblJobTamins { get; set; }
        public virtual DbSet<TblLeaveJob> TblLeaveJobs { get; set; }
        public virtual DbSet<TblLink> TblLinks { get; set; }
        public virtual DbSet<TblMilitary> TblMilitaries { get; set; }
        public virtual DbSet<TblMilitaryOrganization> TblMilitaryOrganizations { get; set; }
        public virtual DbSet<TblPageTimeLog> TblPageTimeLogs { get; set; }
        public virtual DbSet<TblPagesSequence> TblPagesSequences { get; set; }
        public virtual DbSet<TblPrimaryInformation> TblPrimaryInformations { get; set; }
        public virtual DbSet<TblSkill> TblSkills { get; set; }
        public virtual DbSet<TblSmsReceived> TblSmsReceiveds { get; set; }
        public virtual DbSet<TblSmsSent> TblSmsSents { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblUserJob> TblUserJobs { get; set; }
        public virtual DbSet<TblUserMilitary> TblUserMilitaries { get; set; }
        public virtual DbSet<TblUserSkill> TblUserSkills { get; set; }
        public virtual DbSet<TblWorkExperience> TblWorkExperiences { get; set; }
        public virtual DbSet<TblWorkExperienceLeaveJobDtl> TblWorkExperienceLeaveJobDtls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=.;Database=EmployDB;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer("server=.;database=ShortLinkDB;User Id=sa;Password=S33@||;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Persian_100_CI_AI");

            modelBuilder.Entity<PayDiploma>(entity =>
            {
                entity.HasKey(e => e.DiplomaId);

                entity.ToTable("Pay_Diploma");

                entity.HasIndex(e => e.DiplomaName, "IX_Pay_Diploma")
                    .IsUnique();

                entity.Property(e => e.DiplomaId).HasColumnName("Diploma_ID");

                entity.Property(e => e.DiplomaCode).HasColumnName("Diploma_Code");

                entity.Property(e => e.DiplomaName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Diploma_Name");
            });

            modelBuilder.Entity<PayEducation>(entity =>
            {
                entity.HasKey(e => e.EducationId);

                entity.ToTable("Pay_Education");

                entity.HasIndex(e => e.EducationName, "IX_Pay_Education")
                    .IsUnique();

                entity.Property(e => e.EducationId).HasColumnName("Education_ID");

                entity.Property(e => e.EducationName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Education_Name")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TblCustomerDegree>(entity =>
            {
                entity.HasKey(e => e.FldCustomerDegreeId);

                entity.ToTable("Tbl_CustomerDegree");

                entity.Property(e => e.FldCustomerDegreeId).HasColumnName("Fld_CustomerDegreeID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.DiplomaId).HasColumnName("Diploma_ID");

                entity.Property(e => e.EducationId).HasColumnName("Education_ID");

                entity.Property(e => e.FldDes)
                    .HasMaxLength(500)
                    .HasColumnName("Fld_Des");

                entity.Property(e => e.FldEducationName)
                    .HasMaxLength(100)
                    .HasColumnName("Fld_EducationName");

                entity.Property(e => e.FldEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Fld_EndDate");

                entity.Property(e => e.FldExportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Fld_ExportDate");

                entity.Property(e => e.FldExportNo)
                    .HasMaxLength(50)
                    .HasColumnName("Fld_ExportNO");

                entity.Property(e => e.FldPoint)
                    .HasMaxLength(5)
                    .HasColumnName("Fld_Point");

                entity.Property(e => e.FldStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Fld_StartDate");

                entity.Property(e => e.FldStudyCity)
                    .HasMaxLength(50)
                    .HasColumnName("Fld_StudyCity");

                entity.Property(e => e.FldStudyPlace)
                    .HasMaxLength(50)
                    .HasColumnName("Fld_StudyPlace");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.Diploma)
                    .WithMany(p => p.TblCustomerDegrees)
                    .HasForeignKey(d => d.DiplomaId)
                    .HasConstraintName("FK_Tbl_CustomerDegree_Pay_Diploma");

                entity.HasOne(d => d.Education)
                    .WithMany(p => p.TblCustomerDegrees)
                    .HasForeignKey(d => d.EducationId)
                    .HasConstraintName("FK_Tbl_CustomerDegree_Pay_Education");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblCustomerDegrees)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_CustomerDegree_Tbl_User");
            });

            modelBuilder.Entity<TblHowFind>(entity =>
            {
                entity.ToTable("Tbl_HowFind");

                entity.Property(e => e.AdditionalDes).HasMaxLength(1000);

                entity.Property(e => e.HowFindTitle)
                    .HasMaxLength(50)
                    .HasColumnName("HowFind_Title");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblHowFinds)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_HowFind_Tbl_User");
            });

            modelBuilder.Entity<TblIpLog>(entity =>
            {
                entity.ToTable("Tbl_IpLog");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Ip).HasMaxLength(100);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblIpLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_IpLog_Tbl_User1");
            });

            modelBuilder.Entity<TblJob>(entity =>
            {
                entity.ToTable("Tbl_Jobs");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.JobTitle).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblJobTamin>(entity =>
            {
                entity.HasKey(e => e.FldTaminJobId);

                entity.ToTable("Tbl_JobTamin");

                entity.Property(e => e.FldTaminJobId).HasColumnName("Fld_TaminJobID");

                entity.Property(e => e.FldTaminJobCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Fld_TaminJobCode");

                entity.Property(e => e.FldTaminJobName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Fld_TaminJobName");

                entity.Property(e => e.FldTaminJobStatus).HasColumnName("Fld_TaminJobStatus");

                entity.Property(e => e.FldTaminJobStatusDate)
                    .HasMaxLength(50)
                    .HasColumnName("Fld_TaminJobStatusDate");
            });

            modelBuilder.Entity<TblLeaveJob>(entity =>
            {
                entity.HasKey(e => e.FldLeaveJobId);

                entity.ToTable("Tbl_LeaveJob");

                entity.Property(e => e.FldLeaveJobId)
                    .ValueGeneratedNever()
                    .HasColumnName("Fld_LeaveJobID");

                entity.Property(e => e.FldLeaveJobTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Fld_LeaveJobTitle");
            });

            modelBuilder.Entity<TblLink>(entity =>
            {
                entity.ToTable("Tbl_Link");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<TblMilitary>(entity =>
            {
                entity.ToTable("Tbl_Military");

                entity.Property(e => e.MilitaryStatus).HasMaxLength(50);
            });

            modelBuilder.Entity<TblMilitaryOrganization>(entity =>
            {
                entity.ToTable("Tbl_MilitaryOrganization");

                entity.Property(e => e.OrganizationName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblPageTimeLog>(entity =>
            {
                entity.ToTable("Tbl_PageTimeLog");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.PageLevel).HasMaxLength(10);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblPageTimeLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_PageTimeLog_Tbl_User");
            });

            modelBuilder.Entity<TblPagesSequence>(entity =>
            {
                entity.ToTable("Tbl_PagesSequence");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblPrimaryInformation>(entity =>
            {
                entity.ToTable("Tbl_PrimaryInformation");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Marital).HasMaxLength(20);

                entity.Property(e => e.NationalCode).HasMaxLength(20);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblPrimaryInformations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_PrimaryInformation_Tbl_User");
            });

            modelBuilder.Entity<TblSkill>(entity =>
            {
                entity.ToTable("Tbl_Skills");

                entity.Property(e => e.SkillTitle).HasMaxLength(100);
            });

            modelBuilder.Entity<TblSmsReceived>(entity =>
            {
                entity.ToTable("Tbl_SmsReceived");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Message).HasMaxLength(10);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<TblSmsSent>(entity =>
            {
                entity.ToTable("Tbl_SmsSent");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Message).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("Tbl_User");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CurrentLevel).HasMaxLength(20);

                entity.Property(e => e.PagesSequenceId).HasColumnName("PagesSequence_Id");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.TrackNo).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.PagesSequence)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.PagesSequenceId)
                    .HasConstraintName("FK_Tbl_User_Tbl_PagesSequence");
            });

            modelBuilder.Entity<TblUserJob>(entity =>
            {
                entity.ToTable("Tbl_UserJob");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Job_Title");

                entity.Property(e => e.JobsId).HasColumnName("Jobs_Id");

                entity.Property(e => e.RequestMoney).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.Jobs)
                    .WithMany(p => p.TblUserJobs)
                    .HasForeignKey(d => d.JobsId)
                    .HasConstraintName("FK_UserJob_Jobs");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserJobs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_UserJob_Tbl_User");
            });

            modelBuilder.Entity<TblUserMilitary>(entity =>
            {
                entity.ToTable("Tbl_UserMilitary");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ExemptDescription).HasMaxLength(500);

                entity.Property(e => e.MilitaryId).HasColumnName("Military_Id");

                entity.Property(e => e.OrganizationId).HasColumnName("Organization_Id");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Unit).HasMaxLength(200);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.Military)
                    .WithMany(p => p.TblUserMilitaries)
                    .HasForeignKey(d => d.MilitaryId)
                    .HasConstraintName("FK_Tbl_UserMilitary_Tbl_Military");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.TblUserMilitaries)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Tbl_UserMilitary_Tbl_MilitaryOrganization");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserMilitaries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_UserMilitary_Tbl_User");
            });

            modelBuilder.Entity<TblUserSkill>(entity =>
            {
                entity.ToTable("Tbl_UserSkill");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LicenseNo).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.SkillId).HasColumnName("Skill_Id");

                entity.Property(e => e.SkillTitle).HasMaxLength(100);

                entity.Property(e => e.SkillType).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.TblUserSkills)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_Tbl_UserSkill_Skills");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserSkills)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_UserSkill_Tbl_User");
            });

            modelBuilder.Entity<TblWorkExperience>(entity =>
            {
                entity.HasKey(e => e.FldWorkExperienceId);

                entity.ToTable("Tbl_WorkExperience");

                entity.Property(e => e.FldWorkExperienceId).HasColumnName("Fld_WorkExperienceID");

                entity.Property(e => e.FldAmountOfDailyInsurance)
                    .HasColumnName("Fld_AmountOfDailyInsurance")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FldCompanyName)
                    .HasMaxLength(200)
                    .HasColumnName("Fld_CompanyName");

                entity.Property(e => e.FldContactInnerNumberOfCompany)
                    .HasMaxLength(10)
                    .HasColumnName("Fld_ContactInnerNumberOfCompany");

                entity.Property(e => e.FldContactNumberOfCompany)
                    .HasMaxLength(50)
                    .HasColumnName("Fld_ContactNumberOfCompany");

                entity.Property(e => e.FldCustomerName)
                    .HasMaxLength(100)
                    .HasColumnName("Fld_CustomerName");

                entity.Property(e => e.FldDescription).HasColumnName("Fld_Description");

                entity.Property(e => e.FldEarlySalary)
                    .HasMaxLength(50)
                    .HasColumnName("Fld_EarlySalary");

                entity.Property(e => e.FldEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Fld_EndDate");

                entity.Property(e => e.FldJobTitle)
                    .HasMaxLength(100)
                    .HasColumnName("Fld_JobTitle");

                entity.Property(e => e.FldLateSalary)
                    .HasMaxLength(50)
                    .HasColumnName("Fld_LateSalary");

                entity.Property(e => e.FldLeaveJobId).HasColumnName("Fld_LeaveJobID");

                entity.Property(e => e.FldReasonsToLeaveJob).HasColumnName("Fld_ReasonsToLeaveJob");

                entity.Property(e => e.FldRelatedPeople)
                    .HasMaxLength(100)
                    .HasColumnName("Fld_RelatedPeople");

                entity.Property(e => e.FldStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Fld_StartDate");

                entity.Property(e => e.FldTaminJobId).HasColumnName("Fld_TaminJobID");

                entity.Property(e => e.FldUnitName)
                    .HasMaxLength(100)
                    .HasColumnName("Fld_UnitName");

                entity.Property(e => e.FldWorkDay)
                    .HasMaxLength(100)
                    .HasColumnName("Fld_WorkDay");

                entity.Property(e => e.FldWorkTime)
                    .HasMaxLength(100)
                    .HasColumnName("Fld_WorkTime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.FldLeaveJob)
                    .WithMany(p => p.TblWorkExperiences)
                    .HasForeignKey(d => d.FldLeaveJobId)
                    .HasConstraintName("FK_Tbl_WorkExperience_Tbl_LeaveJob");

                entity.HasOne(d => d.FldTaminJob)
                    .WithMany(p => p.TblWorkExperiences)
                    .HasForeignKey(d => d.FldTaminJobId)
                    .HasConstraintName("FK_Tbl_WorkExperience_Tbl_JobTamin");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblWorkExperiences)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_WorkExperience_Tbl_User");
            });

            modelBuilder.Entity<TblWorkExperienceLeaveJobDtl>(entity =>
            {
                entity.HasKey(e => e.FldWorkExperienceLeaveJobDtlId);

                entity.ToTable("Tbl_WorkExperienceLeaveJobDtl");

                entity.Property(e => e.FldWorkExperienceLeaveJobDtlId).HasColumnName("Fld_WorkExperienceLeaveJobDtlID");

                entity.Property(e => e.FldLeaveJob).HasColumnName("Fld_LeaveJob");

                entity.Property(e => e.FldLeaveJobId).HasColumnName("Fld_LeaveJobID");

                entity.Property(e => e.FldLeaveJobPercent).HasColumnName("Fld_LeaveJobPercent");

                entity.Property(e => e.FldWorkExperienceId).HasColumnName("Fld_WorkExperienceID");

                entity.HasOne(d => d.FldLeaveJobNavigation)
                    .WithMany(p => p.TblWorkExperienceLeaveJobDtls)
                    .HasForeignKey(d => d.FldLeaveJobId)
                    .HasConstraintName("FK_Tbl_WorkExperienceLeaveJobDtl_Tbl_LeaveJob");

                entity.HasOne(d => d.FldWorkExperience)
                    .WithMany(p => p.TblWorkExperienceLeaveJobDtls)
                    .HasForeignKey(d => d.FldWorkExperienceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_WorkExperienceLeaveJobDtl_Tbl_WorkExperience");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
