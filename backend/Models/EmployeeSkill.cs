
#nullable disable

namespace AeDirectory.Models
{
    public partial class EmployeeSkill
    {
        public int EmployeeNumber { get; set; }
        public string SkillId { get; set; }

        public virtual Employee EmployeeNumberNavigation { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
