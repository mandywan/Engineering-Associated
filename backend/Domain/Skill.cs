using System;

namespace AeDirectory
{
    public class Skill
    {

        public int Skill_id { get; set; }
        public int Category_id { get; set; }
        public string Label { get; set; }
        public string SortValue { get; set; }
        public Employee[] Employees { get; set; }

        public Skill(int skill_id, int category_id, string label, string sortValue, Employee[] employees)
        {
            Skill_id = skill_id;
            Category_id = category_id;
            Label = label;
            SortValue = sortValue;
            Employees = employees;
        }
    }
}
