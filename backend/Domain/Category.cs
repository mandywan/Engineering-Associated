using System;
using System.Collections.Generic;

#nullable enable

namespace AeDirectory.Domain
{
    public class Category
    {

        public string? Category_id { get; set; }
        public string? Label { get; set; }
        public string? SortValue { get; set; }
        public List<Skill>? Skills { get; set; }
        // public Category(string? category_id, string? label, string? sortValue, List<Skill> skills)
        // {
        //     Category_id = category_id;
        //     Label = label;
        //     SortValue = sortValue;
        //     Skills = skills;
        // }
    }
}
