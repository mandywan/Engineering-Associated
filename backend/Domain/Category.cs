using System;

namespace AeDirectory
{
    public class Category
    {

        public int Category_id { get; set; }
        public string Label { get; set; }
        public string SortValue { get; set; }
        public Skill[] Skills { get; set; }

        public Category(int category_id, string label, string sortValue, Skill[] skills)
        {
            Category_id = category_id;
            Label = label;
            SortValue = sortValue;
            Skills = skills;
        }
    }
}
