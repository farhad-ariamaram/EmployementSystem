using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{
    public class TblWorkExperienceMD
    {
        [Required(ErrorMessage = "نام شرکت اجباری است")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "طول نام شرکت حداقل 2 و حداکثر 200 کاراکتر می‌باشد")]
        public string FldCompanyName { get; set; }

        [Required(ErrorMessage = "شماره تلفن شرکت اجباری است")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "طول شماره تلفن شرکت حداقل 2 و حداکثر 50 کاراکتر می‌باشد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده  شماره تلفن شرکت صحیح نمی‌باشد")]
        public string FldContactNumberOfCompany { get; set; }

        [Required(ErrorMessage = "شماره داخلی شرکت اجباری است")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "طول شماره داخلی شرکت حداقل 1 و حداکثر 10 کاراکتر می‌باشد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده شماره داخلی شرکت صحیح نمی‌باشد")]
        public string FldContactInnerNumberOfCompany { get; set; }

        [Required(ErrorMessage = "معرفی نفر مرتبط اجباری است")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "طول فیلد معرفی نفر مرتبط حداقل 2 و حداکثر 100 کاراکتر می‌باشد")]
        public string FldRelatedPeople { get; set; }

        [Required(ErrorMessage = "مقدار اولین حقوق دریافتی اجباری است")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "طول فیلد مقدار اولین حقوق دریافتی حداقل 1 و حداکثر 50 کاراکتر می‌باشد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده مقدار اولین حقوق دریافتی صحیح نمی‌باشد")]
        public string FldEarlySalary { get; set; }

        [Required(ErrorMessage = "آخرین حقوق دریافتی اجباری است")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "طول فیلد آخرین حقوق دریافتی حداقل 1 و حداکثر 50 کاراکتر می‌باشد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده آخرین حقوق دریافتی صحیح نمی‌باشد")]
        public string FldLateSalary { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده تعداد روز بیمه شده صحیح نمی‌باشد")]
        public long? FldAmountOfDailyInsurance { get; set; }

        [Required(ErrorMessage = "مقدار ساعت کاری اجباری است")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده مقدار ساعت کاری صحیح نمی‌باشد")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "طول ساعت کاری حداقل 1 و حداکثر 100 کاراکتر می‌باشد")]
        public string FldWorkTime { get; set; }

        [Required(ErrorMessage = "مقدار روز های کاری اجباری است")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فرمت وارد شده مقدار روز های کاری صحیح نمی‌باشد")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "طول مقدار روز های کاری حداقل 1 و حداکثر 100 کاراکتر می‌باشد")]
        public string FldWorkDay { get; set; }

        [Required(ErrorMessage = "نام واحد اجباری است")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "طول نام واحد حداقل 2 و حداکثر 100 کاراکتر می‌باشد")]
        public string FldUnitName { get; set; }

        [StringLength(500, ErrorMessage = "طول این فیلد حداکثر 500 کاراکتر می‌باشد")]
        public string Description { get; set; }
    }


    [ModelMetadataType(typeof(TblWorkExperienceMD))]
    public partial class TblWorkExperience
    {

    }
}
