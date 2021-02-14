using System;

namespace AeDirectory
{
    public class Skill
    {

        public int skill_id { get; set; }
        public int category_id { get; set; }
        public string label { get; set; }
        public string sortValue { get; set; }
        public Employee[] employees { get; set; }


    }
}
