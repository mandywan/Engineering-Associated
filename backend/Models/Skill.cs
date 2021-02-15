using System;
using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
/**
* If anyone wants to change this class, please discord @Ricky
* But the recommendation is that inheriting this class and making a new one by any change. 
*/
    public partial class Skill
    {
        public Skill()
        {
            EmployeeSkills = new HashSet<EmployeeSkill>();
        }

        public string SkillCategoryId { get; set; }
        public string SkillId { get; set; }
        public string Label { get; set; }
        public string SortValue { get; set; }

        public virtual Category SkillCategory { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
