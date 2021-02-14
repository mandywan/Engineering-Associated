using System;
using System.Collections.Generic;

namespace AeDirectory
{
    public class Group
    {
        public int Group_id { get; set; }
        public int Office_id { get; set; }
        public int Comapny_id { get; set; }
        public string Label { get; set; }
        public Employee Manager { get; set; }

        public Group(int group_id, int office_id, int comapny_id, string label, Employee manager)
        {
            Group_id = group_id;
            Office_id = office_id;
            Comapny_id = comapny_id;
            Label = label;
            Manager = manager;
        }

    }
}
