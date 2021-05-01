using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.Models
{
    public partial class TblCreativityType
    {
        public TblCreativityType()
        {
            TblUserCreativities = new HashSet<TblUserCreativity>();
        }

        public int Id { get; set; }
        public string CreativityType { get; set; }

        public virtual ICollection<TblUserCreativity> TblUserCreativities { get; set; }
    }
}
