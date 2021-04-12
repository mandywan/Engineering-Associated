using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
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
