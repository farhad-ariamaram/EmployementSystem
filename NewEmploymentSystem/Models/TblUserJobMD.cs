using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{
    public class TblUserJobMD
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string JobTitle { get; set; }

        [StringLength(500, ErrorMessage = "طول این فیلد حداکثر 500 کاراکتر می‌باشد")]
        public string Description { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده صحیح نمی‌باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string RequestMoney { get; set; }
    }

    [ModelMetadataType(typeof(TblUserJobMD))]
    public partial class TblUserJob
    {

    }
}
