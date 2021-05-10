using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.EmployReqModels
{
    public partial class TblEmployeeRequestCompilationType
    {
        public TblEmployeeRequestCompilationType()
        {
            TblEmployeeRequestUserCompilations = new HashSet<TblEmployeeRequestUserCompilation>();
        }

        public int FldEmployeeRequestCompilationTypeId { get; set; }
        public string FldEmployeeRequestCompilationTypeCompilationType { get; set; }

        public virtual ICollection<TblEmployeeRequestUserCompilation> TblEmployeeRequestUserCompilations { get; set; }
    }
}
