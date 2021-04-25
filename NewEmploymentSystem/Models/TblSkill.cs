using System;
using System.Collections.Generic;

#nullable disable

namespace NewEmploymentSystem.Models
{
    public partial class TblSkill
    {
        public TblSkill()
        {
            TblUserSkills = new HashSet<TblUserSkill>();
        }

        public int Id { get; set; }
        public string SkillTitle { get; set; }

        public virtual ICollection<TblUserSkill> TblUserSkills { get; set; }
    }
}
