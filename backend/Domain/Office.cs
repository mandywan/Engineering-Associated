using System.Collections.Generic;

namespace AeDirectory.Domain
{
    public class Office
    {
        public string Office_id { get; set; }
        public string Comapny_id { get; set; }
        public string Label { get; set; }
        public Employee Manager { get; set; }
        public List<Group> Groups { get; set; }

        public Office(string office_id, string comapny_id, string label, Employee manager, List<Group> groups)
        {
            Office_id = office_id;
            Comapny_id = comapny_id;
            Label = label;
            Manager = manager;
            Groups = groups;
        }

    }
}
