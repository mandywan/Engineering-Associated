using System;

namespace AeDirectory
{
    public class Group
    {
        public int group_id { get; set; }
        public int office_id { get; set; }
        public int comapny_id { get; set; }
        public string label { get; set; }
        public Employee manager { get; set; }

    }
}
