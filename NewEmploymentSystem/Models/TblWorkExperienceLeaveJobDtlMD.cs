using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{
    public class TblWorkExperienceLeaveJobDtlMD
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [Range(0, 100, ErrorMessage = "یک عدد بین 0 تا 100 وارد کنید")]
        public int FldLeaveJobPercent { get; set; }
    }

    [ModelMetadataType(typeof(TblWorkExperienceLeaveJobDtlMD))]
    public partial class TblWorkExperienceLeaveJobDtl
    {
    }
}
