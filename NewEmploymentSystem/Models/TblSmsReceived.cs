﻿using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.Models
{
    public partial class TblSmsReceived
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public DateTime? Date { get; set; }
        public string Message { get; set; }
    }
}
