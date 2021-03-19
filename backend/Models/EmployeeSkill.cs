using System;
using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
/**
* If anyone wants to change this class, please discord @Ricky
* But the recommendation is that inheriting this class and making a new one by any change. 
*/
    public partial class EmployeeSkill
    {
        public int EmployeeNumber { get; set; }
        public string SkillId { get; set; }

        public virtual Employee EmployeeNumberNavigation { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
