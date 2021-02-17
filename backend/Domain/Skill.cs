using System;
using System.Collections.Generic;

#nullable enable

namespace AeDirectory.Domain
{
    public class Skill
    {

        public string? Skill_id { get; set; }
        public string? Category_id { get; set; }
        public string? Label { get; set; }
        public string? SortValue { get; set; }
        public List<Employee> Employees { get; set; }

        public Skill(string skill_id, string category_id, string label, string sortValue, List<Employee> employees)
        {
            Skill_id = skill_id;
            Category_id = category_id;
            Label = label;
            SortValue = sortValue;
            Employees = employees;
        }
    }
}
