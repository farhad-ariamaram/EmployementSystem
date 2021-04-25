using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.Models
{
    public partial class TblJob
    {
        public TblJob()
        {
            TblUserJobs = new HashSet<TblUserJob>();
        }

        public int Id { get; set; }
        public bool? IsActive { get; set; }
        public string JobTitle { get; set; }

        public virtual ICollection<TblUserJob> TblUserJobs { get; set; }
    }
}
