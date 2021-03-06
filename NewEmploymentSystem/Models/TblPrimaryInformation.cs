using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.Models
{
    public partial class TblPrimaryInformation
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public int? ChildrenNo { get; set; }
        public string Marital { get; set; }
        public int? Tutelage { get; set; }
        public string PostalCode { get; set; }
        public string TrackNo { get; set; }
        public string Address { get; set; }

        public virtual TblUser User { get; set; }
    }
}
