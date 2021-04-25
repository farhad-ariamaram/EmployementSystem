using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{
    public class TblPrimaryInformationMD
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "طول این فیلد حداقل 3 و حداکثر 25 کاراکتر می‌باشد")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "طول این فیلد حداقل 3 و حداکثر 25 کاراکتر می‌باشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "طول این فیلد باید 10 کاراکتر ‌باشد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده صحیح نمی‌باشد")]
        public string NationalCode { get; set; }
    }

    [ModelMetadataType(typeof(TblPrimaryInformationMD))]
    public partial class TblPrimaryInformation
    {

    }
}
