using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{
    public class TblUserMilitaryMD
    {
        [StringLength(50, ErrorMessage = "طول این فیلد حداکثر 50 کاراکتر می‌باشد")]
        public string City { get; set; }

        [StringLength(200, ErrorMessage = "طول این فیلد حداکثر 200 کاراکتر می‌باشد")]
        public string Unit { get; set; }

        [StringLength(500, ErrorMessage = "طول این فیلد حداکثر 500 کاراکتر می‌باشد")]
        public string ExemptDescription { get; set; }

        [StringLength(500, ErrorMessage = "طول این فیلد حداکثر 500 کاراکتر می‌باشد")]
        public string Description { get; set; }
    }

    [ModelMetadataType(typeof(TblUserMilitaryMD))]
    public partial class TblUserMilitary
    {

    }
}
