using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{


    public class TblUserCompilationMD
    {

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "طول این فیلد حداکثر 1000 کاراکتر می‌باشد")]
        public string Explanation { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string Author { get; set; }

        [StringLength(1000, ErrorMessage = "طول این فیلد حداکثر 1000 کاراکتر می‌باشد")]
        public string Description { get; set; }
    }


    [ModelMetadataType(typeof(TblUserCompilationMD))]
    public partial class TblUserCompilation
    {

    }
}
