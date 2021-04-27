using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NewEmploymentSystem.Models
{
    public class TblCustomerDegreeMD
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "طول این فیلد حداقل 2 و حداکثر 50 کاراکتر می‌باشد")]
        public string FldExportNo { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [RegularExpression("[+-]?([0-9]*[.])?[0-9]+", ErrorMessage = "فرمت وارد شده صحیح نمی‌باشد")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        [Range(10.0, 20.0, ErrorMessage = "یک عدد بین 10 تا 20 وارد کنید")]

        public string FldPoint { get; set; }

        [StringLength(500, ErrorMessage = "طول این فیلد حداکثر 500 کاراکتر می‌باشد")]
        public string FldDes { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "طول این فیلد حداقل 2 و حداکثر 50 کاراکتر می‌باشد")]
        public string FldStudyPlace { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "طول این فیلد حداقل 2 و حداکثر 50 کاراکتر می‌باشد")]
        public string FldStudyCity { get; set; }
    }

    [ModelMetadataType(typeof(TblCustomerDegreeMD))]
    public partial class TblCustomerDegree
    {
    }
}
