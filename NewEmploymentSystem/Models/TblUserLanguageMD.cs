using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Models
{

    public class TblUserLanguageMD
    {

        [StringLength(1000, ErrorMessage = "طول این فیلد حداکثر 1000 کاراکتر می‌باشد")]
        public string Description { get; set; }
    }


    [ModelMetadataType(typeof(TblUserLanguageMD))]
    public partial class TblUserLanguage
    {

    }
}
