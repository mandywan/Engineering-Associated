using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{ 
    public partial class Category
    {
        public Category()
        {
            Skills = new HashSet<Skill>();
        }

        public string SkillCategoryId { get; set; }
        public string Label { get; set; }
        public string SortValue { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
