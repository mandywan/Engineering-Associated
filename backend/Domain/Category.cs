using System;

namespace AeDirectory
{
    public class Category
    {

        public int category_id { get; set; }
        public string label { get; set; }
        public string sortValue { get; set; }
        public Skill[] skills { get; set; }


    }
}
