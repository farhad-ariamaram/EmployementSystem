using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.EmployReqModels
{
    public partial class TblEmployeeRequestLanguageType
    {
        public TblEmployeeRequestLanguageType()
        {
            TblEmployeeRequestUserLanguages = new HashSet<TblEmployeeRequestUserLanguage>();
        }

        public int FldEmployeeRequestLanguageTypeId { get; set; }
        public string FldEmployeeRequestLanguageTypeLanguageType { get; set; }

        public virtual ICollection<TblEmployeeRequestUserLanguage> TblEmployeeRequestUserLanguages { get; set; }
    }
}
