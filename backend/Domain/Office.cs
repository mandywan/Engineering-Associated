using System;

namespace AeDirectory
{
    public class Office
    {
        public int Office_id { get; set; }
        public int Comapny_id { get; set; }
        public string Label { get; set; }
        public Employee Manager { get; set; }
        public Group[] Groups { get; set; }

        public Office(int office_id, int comapny_id, string label, Employee manager, Group[] groups)
        {
            Office_id = office_id;
            Comapny_id = comapny_id;
            Label = label;
            Manager = manager;
            Groups = groups;
        }

    }
}
