using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace NewEmploymentSystem.Models
{
    public class TblEmergencyCallMD
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string Relative { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده صحیح نمی‌باشد")]
        public string PhoneNo { get; set; }

        [StringLength(1000, ErrorMessage = "طول این فیلد حداکثر 1000 کاراکتر می‌باشد")]
        public string Description { get; set; }
    }


    [ModelMetadataType(typeof(TblEmergencyCallMD))]
    public partial class TblEmergencyCall
    {

    }
}
