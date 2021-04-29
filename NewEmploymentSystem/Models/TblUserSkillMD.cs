using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{
    public class TblUserSkillMD
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(100, ErrorMessage = "طول این فیلد حداکثر 100 کاراکتر می‌باشد")]
        public string SkillTitle { get; set; }

        [StringLength(500, ErrorMessage = "طول این فیلد حداکثر 500 کاراکتر می‌باشد")]
        public string Description { get; set; }

        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string Location { get; set; }

        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string LicenseNo { get; set; }

    }

    [ModelMetadataType(typeof(TblUserSkillMD))]
    public partial class TblUserSkill
    {

    }
}
