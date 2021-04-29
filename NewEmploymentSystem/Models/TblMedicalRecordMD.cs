using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{
    public class TblMedicalRecordMD
    {
        [StringLength(1000, ErrorMessage = "طول این فیلد حداکثر 1000 کاراکتر می‌باشد")]
        public string ProblemDes { get; set; }

        [StringLength(1000, ErrorMessage = "طول این فیلد حداکثر 1000 کاراکتر می‌باشد")]
        public string Description { get; set; }

        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string Disease { get; set; }

        [StringLength(1000, ErrorMessage = "طول این فیلد حداکثر 1000 کاراکتر می‌باشد")]
        public string ComplicationsDes { get; set; }
    }

    [ModelMetadataType(typeof(TblMedicalRecordMD))]
    public partial class TblMedicalRecord
    {
    }
}
