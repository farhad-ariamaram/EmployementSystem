using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.Models
{
    public partial class TblIpLog
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Ip { get; set; }
        public DateTime? DateTime { get; set; }

        public virtual TblUser User { get; set; }
    }
}
