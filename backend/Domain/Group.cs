using System;
using System.Collections.Generic;

namespace AeDirectory.Domain
{
    public class Group
    {
        public string Group_id { get; set; }
        public string Office_id { get; set; }
        public string Comapny_id { get; set; }
        public string Label { get; set; }
        public Employee Manager { get; set; }

        public Group(string group_id, string office_id, string comapny_id, string label, Employee manager)
        {
            Group_id = group_id;
            Office_id = office_id;
            Comapny_id = comapny_id;
            Label = label;
            Manager = manager;
        }

    }
}
