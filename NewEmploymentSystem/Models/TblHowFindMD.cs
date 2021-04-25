using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{
    public class TblHowFindMD
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string HowFindTitle { get; set; }

        [StringLength(1000, ErrorMessage = "طول این فیلد حداکثر 1000 کاراکتر می‌باشد")]
        public string AdditionalDes { get; set; }
    }

    [ModelMetadataType(typeof(TblHowFindMD))]
    public partial class TblHowFind
    {
    }
}
